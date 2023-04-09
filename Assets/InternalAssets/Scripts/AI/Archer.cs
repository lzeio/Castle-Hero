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
   private StatSystem statSystem;

    private List<GameObject> arrowList;
    void Start()
    {
        characterStats = GetComponent<StatSystem>();
        animationController = GetComponent<AnimationController>();
        characterStats.OnDeath += OnDeath;
    }

    void Update()
    {
        raycastPoint = new Vector3(transform.position.x, transform.position.y + .75f, transform.position.z);
        if (Physics.Raycast(raycastPoint, transform.forward, out RaycastHit hitInfo, characterStats.characterData.AttackRange))
        {
            if (hitInfo.collider.TryGetComponent(out StatSystem statSystem) && hitInfo.transform.gameObject.layer!= this.gameObject.layer )
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

    private void SpawnArrow()
    {
        GameObject arrow = Instantiate(characterStats.characterData.Projectile, this.transform);
        arrow.transform.localScale = Vector3.one;
        AttackPoint arrowAttack = arrow.GetComponent<AttackPoint>();
        arrowAttack.SetStatsData(characterStats);
        ShootProjectile(arrow);
    }
    private void SpawnBall()
    {
        GameObject ball = Instantiate(characterStats.characterData.Projectile, this.transform);
        AttackPoint ballAttack = ball.GetComponent<AttackPoint>();
        ballAttack.SetStatsData(characterStats);
        ShootProjectile(ball);
    }

    public void ShootProjectile(GameObject projectile)
    {
        projectile.transform.DOLocalMoveZ(projectile.transform.position.z * characterStats.characterData.AttackRange, 10f);
    }
    private void OnDeath()
    {
        animationController.ResetAnimation();
        animationController.Death();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (characterStats != null)
            Debug.DrawRay(raycastPoint, transform.forward * 10f, Color.red);
    }
}
