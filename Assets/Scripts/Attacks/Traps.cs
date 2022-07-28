using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    public float health; 
    public int damage;
    public void DamageTaken(float damage)
    {
        Debug.Log("Called");
        health -= damage;

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 7 )
        { 
            other.GetComponent<HealthSystem>().DamageTaken(damage);
        }
    }
}
