using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFrySword : Enemy   
{
    public EnemyScriptable smallFryData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Castle"))
        {
            collision.gameObject.GetComponent<Castle>().DamageTaken(smallFryData.attack);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Shield"))
        {
            other.gameObject.GetComponent<Shield>().DamageTaken(smallFryData.attack);
        }
        if (other.gameObject.CompareTag("Traps"))
        {
            other.GetComponent<Traps>().DamageTaken(smallFryData.attack);
        }
    }

}
