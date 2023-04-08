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

    private void SpawnArrow()
    {
        GameObject arrow = Instantiate(characterStats.characterData.Projectile, transform.GetChild(2).transform.position, Quaternion.identity);
        AttackPoint arrowAttack = arrow.GetComponent<AttackPoint>();
        arrowAttack.SetStatsData(characterStats);
        arrow.transform.SetParent(transform.GetChild(2).transform, false);
        arrow.transform.parent = null;
        arrow.transform.localScale = characterStats.characterData.Projectile.transform.localScale;
        ShootProjectile(arrow);
    }
    private void SpawnBall()
    {
        GameObject ball = Instantiate(characterStats.characterData.Projectile, transform.GetChild(2).transform.position, Quaternion.identity);
        AttackPoint ballAttack = ball.GetComponent<AttackPoint>();
        ballAttack.SetStatsData(characterStats);
        ball.transform.SetParent(transform.GetChild(2).transform, false);
        ball.transform.parent = null;
        ball.transform.localScale = characterStats.characterData.Projectile.transform.localScale;
        ShootProjectile(ball);
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (statSystem != null)
            Debug.DrawRay(raycastPoint, transform.forward * statSystem.characterData.AttackRange, Color.yellow);
    }
}
