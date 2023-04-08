using DG.Tweening;
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
    }

    // Update is called once per frame
    void Update()
    {
        raycastPoint = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1.5f);
        if (Physics.Raycast(raycastPoint, transform.forward, out RaycastHit hitInfo, characterStats.characterData.AttackRange))
        {
            if (hitInfo.collider.TryGetComponent(out StatSystem statSystem))
            {
                SpawnCannon();
            }
        }
        
    }
    private void SpawnCannon()
    {
        if (canShoot)
        {
            canShoot = false;
            GameObject arrow = Instantiate(characterStats.characterData.Projectile, characterStats.characterData.Projectile.transform.position, Quaternion.identity, this.transform);
            AttackPoint arrowAttack = arrow.GetComponent<AttackPoint>();
            arrowAttack.SetStatsData(characterStats);
            ShootProjectile(arrow);
            DOVirtual.DelayedCall(2f, () => canShoot = true);
        }
    }

    public void ShootProjectile(GameObject projectile)
    {
        projectile.transform.DOMove(transform.forward * characterStats.characterData.AttackRange, 10f);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (characterStats != null)
            Debug.DrawRay(raycastPoint, transform.forward * characterStats.characterData.AttackRange, Color.yellow);
    }
}
