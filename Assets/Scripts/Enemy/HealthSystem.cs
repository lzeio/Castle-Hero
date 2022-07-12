using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    public EnemyScriptable enemy;
    public float health;

    public event EventHandler OnDeath;
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
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    void Death()
    {
        this.gameObject.SetActive(false);
        if(gameObject.CompareTag("Heavy"))
        {
            GetComponent<Heavy>().anim.SetBool("Dead", true);
        }
    }
}
