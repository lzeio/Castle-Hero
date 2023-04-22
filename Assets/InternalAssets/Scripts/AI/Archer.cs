using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatSystem))]
[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Archer : MonoBehaviour
{
    private AnimationController animationController;
    private StatSystem characterStats;
    private Vector3 raycastPoint;

    private List<GameObject> arrowList;

    public bool canShoot = true;
    void Start()
    {
        characterStats = GetComponent<StatSystem>();
        animationController = GetComponent<AnimationController>();
        characterStats.OnDeath += OnDeath;
    }

    void Update()
    {
        raycastPoint = new Vector3(transform.position.x, transform.position.y + 1.75f, transform.position.z);
        if (Physics.Raycast(raycastPoint, transform.forward,characterStats.characterData.AttackRange))
        {
            if (canShoot)
            {
                canShoot = false;
                animationController.Attack();
                DOVirtual.DelayedCall(1f, () => canShoot = true);
            }
        }
        else
        {
            if (characterStats.characterData.Movable)
            {
                animationController.ResetAnimation();
                animationController.Move();
                transform.position += (transform.forward * characterStats.characterData.Speed * Time.deltaTime);
            }
            else
            {
                animationController.ResetAnimation();
                animationController.Idle();
            }
        }
    }


    private void OnDestroy()
    {
        characterStats.OnDeath -= OnDeath;
    }

    private void SpawnArrow()
    {
        GameObject arrow = Instantiate(characterStats.characterData.Projectile, this.transform);
        arrow.transform.localScale = Vector3.one;
        AttackPoint arrowAttack = arrow.GetComponent<AttackPoint>();
        arrowAttack.SetStatsData(characterStats);
        arrow.layer = this.gameObject.layer;
        
        arrow.transform.DOMoveZ(arrow.transform.position.z * characterStats.characterData.AttackRange, 5f);
    }

    private void SpawnBall()
    {
        GameObject ball = Instantiate(characterStats.characterData.Projectile, this.transform);
        AttackPoint ballAttack = ball.GetComponent<AttackPoint>();
        ballAttack.SetStatsData(characterStats);
        ball.layer = this.gameObject.layer;
        
        ball.transform.DOMoveZ(-ball.transform.position.z * characterStats.characterData.AttackRange, 50f, false);
    }

   
    private void OnDeath()
    {
        animationController.ResetAnimation();
        animationController.Death();
        transform.DOScale(0, 1f).OnComplete(() => Destroy(gameObject));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (characterStats != null)
            Debug.DrawRay(raycastPoint, transform.forward * characterStats.characterData.AttackRange, Color.red);
    }
}
