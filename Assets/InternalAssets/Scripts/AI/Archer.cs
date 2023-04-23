using DG.Tweening;
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
    public RaycastHit[] hits;
    void Start()
    {
        characterStats = GetComponent<StatSystem>();
        animationController = GetComponent<AnimationController>();
        characterStats.OnDeath += OnDeath;
    }

    void Update()
    {
        raycastPoint = new Vector3(transform.position.x, transform.position.y + 1.75f, transform.position.z);
        hits = Physics.RaycastAll(raycastPoint, transform.forward, characterStats.characterData.AttackRange);
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.gameObject.layer == this.gameObject.layer)
            {
                continue;
            }
            if (canShoot && hit.transform.gameObject.layer != this.gameObject.layer)
            {
                canShoot = false;
                animationController.Attack();
                DOVirtual.DelayedCall(1f, () => canShoot = true);
                return;
            }

        }

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


    private void OnDestroy()
    {
        characterStats.OnDeath -= OnDeath;
        DOTween.KillAll();
    }

    private void SpawnArrow()
    {
        GameObject arrow = Instantiate(characterStats.characterData.Projectile, this.transform);
        arrow.transform.localScale = Vector3.one;
        AttackPoint arrowAttack = arrow.GetComponent<AttackPoint>();
        arrowAttack.SetStatsData(characterStats);
        arrow.layer = this.gameObject.layer;
        arrow.transform.DOMoveZ(transform.position.z * characterStats.characterData.AttackRange, 10f);
        DOVirtual.DelayedCall(10f, () => { arrow.transform.DOScale(0, 1f); }) ;
    }

    private void SpawnBall()
    {
        GameObject ball = Instantiate(characterStats.characterData.Projectile, this.transform);
        AttackPoint ballAttack = ball.GetComponent<AttackPoint>();
        ballAttack.SetStatsData(characterStats);
        ball.layer = this.gameObject.layer;
        ball.transform.DOMoveZ(-transform.position.z * characterStats.characterData.AttackRange, 200f);
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
