using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    public EnemyScriptable enemy;
    public float health;

    public event EventHandler OnDeath;

    public List<int> burnTickTimers = new();
    // Start is called before the first frame update
    void Start()
    {
        health = enemy.health;

        OnDeath += Death;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            OnDeath?.Invoke(this, EventArgs.Empty);

           
        }
    }

    public void DamageTaken(int damage)
    {
        health -= damage;
    }

    public void FireDamage(int ticks)
    {
        if(burnTickTimers.Count<=0)
        {
            burnTickTimers.Add(ticks);
            StartCoroutine(Fire());
        }
        else
        {
            burnTickTimers.Add(ticks);
        }
    }

    IEnumerator Fire()
    {
        while(burnTickTimers.Count>0)
        {
            for (int i = 0; i < burnTickTimers.Count; i++)
            {
                burnTickTimers[i]--;
            }
            health -= 5;
            burnTickTimers.RemoveAll(i => i == 0);

            yield return new WaitForSeconds(.5f);
        }
    }
    public void FreezeDamage()
    {
        
    }
    void Death(object sender, EventArgs e)
    {
        this.gameObject.SetActive(false);
        if(gameObject.CompareTag("Heavy"))
        {
            GetComponent<Heavy>().anim.SetBool("Dead", true);
        }
    }
}
