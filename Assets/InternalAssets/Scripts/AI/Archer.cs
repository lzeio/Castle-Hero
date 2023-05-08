using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatSystem))]
[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Archer : MonoBehaviour
{
    private AudioSource audioSource;
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
        audioSource = GetComponent<AudioSource>();
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
            else
            if (hit.transform.gameObject.layer != this.gameObject.layer)
            {
                animationController.Attack();
              
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
        DOTween.Kill(this);
    }

    private void SpawnArrow()
    {
        GameObject arrow = Instantiate(characterStats.characterData.Projectile, this.transform);
        arrow.transform.localScale = Vector3.one;
        AttackPoint arrowAttack = arrow.GetComponent<AttackPoint>();
        arrowAttack.SetStatsData(characterStats);
        arrow.layer = this.gameObject.layer;
        arrow.transform.DOMoveZ(transform.position.z * characterStats.characterData.AttackRange, 3f).SetUpdate(false);
        audioSource.Play();
        DOVirtual.DelayedCall(10f, () => DestroyProjectiles(arrow)).SetUpdate(false);
    }

    private void SpawnBall()
    {
        GameObject ball = Instantiate(characterStats.characterData.Projectile, this.transform);
        AttackPoint ballAttack = ball.GetComponent<AttackPoint>();
        ballAttack.SetStatsData(characterStats);
        ball.layer = this.gameObject.layer;
        ball.transform.DOMoveZ(-transform.position.z * characterStats.characterData.AttackRange, 6f).SetUpdate(false);
        audioSource.Play();
        DOVirtual.DelayedCall(10f, () => DestroyProjectiles(ball)).SetUpdate(false);
    }


    private void OnDeath(GameObject character)
    {
        animationController.ResetAnimation();
        animationController.Death();
        GameplayManager.Instance.WaveSystem.KilledInAction(character);
        DOVirtual.DelayedCall(1f, () => Destroy(gameObject));
    }
    
    public void DestroyProjectiles(GameObject projectiles)
    {
        if (projectiles != null)
            Destroy(projectiles);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (characterStats != null)
            Debug.DrawRay(raycastPoint, transform.forward * characterStats.characterData.AttackRange, Color.red);
    }
}
