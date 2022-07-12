using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public EnemyScriptable enemy;

    float health;
    // Start is called before the first frame update
    void Start()
    {
        health = enemy.health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int damage)
    {
        health -= damage;

        if(health<=0)
        {
            Death();
        }
    }
    
    void Death()
    {
        this.gameObject.SetActive(false);
    }
}
