using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] GameObject Projectile;
    Transform target;
    [SerializeField] Transform Shootpoint;
    [SerializeField] float EnemyTurnSpeed = 5f;
    float FireRate = 0.2f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        FireRate -= Time.deltaTime;

        Vector3 direction = target.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), EnemyTurnSpeed * Time.deltaTime);

        if (FireRate < 0)
        {
            FireRate = 0.5f;
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(Projectile, Shootpoint.position, Shootpoint.rotation);
    }
}
