using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTarget : MonoBehaviour
{
    //[SerializeField] float damage = 10f;
    Rigidbody rb;
    [SerializeField] float Speed = 500f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //Transform target = GameObject.FindGameObjectsWithTag("Player").transform;
        Transform target = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 bulletAcuracy = new Vector3(Random.Range(0, 0f), Random.Range(0, 0f), Random.Range(0, 0f));
        Vector3 direction = (transform.position - target.position) + bulletAcuracy;
        rb.AddForce(direction * Speed * Time.deltaTime);

        FindObjectOfType<AudioManager>().play("1");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == " player ")
        {
            // playerHealth Effected
            Destroy(gameObject);
        }
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
}
