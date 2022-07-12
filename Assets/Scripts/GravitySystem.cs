using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravitySystem : MonoBehaviour
{

    public Transform globe;
    public float forceAmount = 1000.0f;
    public float gravityRadius = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, globe.position);

        Vector3 targetDirection = (globe.position - transform.position).normalized;   

        if(distance<gravityRadius)
        {
            GetComponent<Rigidbody>().AddForce(targetDirection * forceAmount * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(globe.position, gravityRadius);
    }
}
