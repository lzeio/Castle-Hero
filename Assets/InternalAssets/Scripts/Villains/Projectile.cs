using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Ray ray;
    public GameObject projectile;
    public float velocity = 10f;
    public bool canShoot=true;
    public bool allowInvoke;

    private void Start()
    {
        
    }
    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit) && canShoot )
        {
            
            if (hit.collider.gameObject.CompareTag("Castle") && canShoot)
            {
                canShoot = false;
                GameObject temp = Instantiate(projectile, transform.position, Quaternion.identity);
                temp.SetActive(true);
                hit.collider.gameObject.GetComponent<Castle>().DamageTaken(100);
                projectile.transform.position = transform.position;
                gameObject.transform.position = Vector3.MoveTowards(transform.position, hit.collider.gameObject.transform.position, velocity*Time.deltaTime);
            }

            if(allowInvoke)
            {
                allowInvoke = false;
                Invoke("ResetCanShoot", 1f);
            }
        }
   
    }

    void ResetCanShoot()
    {
        canShoot = true;
        allowInvoke = true;
    }

}
