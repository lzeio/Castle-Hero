using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(StatSystem))]
public class Cannon : MonoBehaviour
{
    private StatSystem characterStats;
    private Vector3 raycastPoint;
    bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        characterStats = GetComponent<StatSystem>();
        characterStats.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        transform.DOScale(0, 1f).OnComplete(() => Destroy(gameObject));
       
    }

    // Update is called once per frame
    void Update()
    {
        raycastPoint = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1.5f);
        if (Physics.Raycast(raycastPoint, transform.forward, out RaycastHit hitInfo, characterStats.characterData.AttackRange))
        {
            if (hitInfo.transform.gameObject.layer!=this.gameObject.layer)
            {
                SpawnCannonBall();
            }
        }
        
    }
    private void OnDestroy()
    {
        characterStats.OnDeath -= OnDeath;
        DOTween.KillAll();  
    }
    private void SpawnCannonBall()
    {
        if (canShoot)
        {
            canShoot = false;
            GameObject arrow = Instantiate(characterStats.characterData.Projectile,raycastPoint, Quaternion.identity, this.transform);
            AttackPoint arrowAttack = arrow.GetComponent<AttackPoint>();
            arrowAttack.SetStatsData(characterStats);
            ShootProjectile(arrow);
            DOVirtual.DelayedCall(2f, () => canShoot = true);
        }
    }

    public void ShootProjectile(GameObject projectile)
    {
        projectile.transform.DOMoveZ(projectile.transform.position.z * characterStats.characterData.AttackRange, 10f);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (characterStats != null)
            Debug.DrawRay(raycastPoint, transform.forward * characterStats.characterData.AttackRange, Color.yellow);
    }
}
