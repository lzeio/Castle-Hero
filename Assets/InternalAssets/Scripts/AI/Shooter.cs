using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


[RequireComponent(typeof(StatSystem))]
[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Shooter : MonoBehaviour
{
    protected AnimationController animationController;
    protected StatSystem characterStats;
    protected Vector3 raycastPoint;
    protected StatSystem statSystem;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        characterStats = GetComponent<StatSystem>();
        animationController = GetComponent<AnimationController>();
        characterStats.OnDeath += OnDeath;
    }

   

    // Update is called once per frame
    protected virtual void Update()
    {
        raycastPoint = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + .5f);
        if (Physics.Raycast(raycastPoint, transform.forward, out RaycastHit hitInfo, characterStats.characterData.AttackRange))
        {
            if (hitInfo.collider.TryGetComponent(out StatSystem statSystem))
            {
                animationController.Attack();
            }
        }
        else
        {
            animationController.ResetAnimation();
            animationController.Idle();
        }
    }

    private void OnDestroy()
    {
        statSystem.OnDeath -= OnDeath;
    }

    public void ShootProjectile(GameObject projectile)
    {
        projectile.transform.DOMove(transform.forward * characterStats.characterData.AttackRange, 10f);
    }

    private void OnDeath()
    {
        animationController.ResetAnimation();
        animationController.Death();
    }

}
