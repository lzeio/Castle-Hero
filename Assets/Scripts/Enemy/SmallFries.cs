using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallFries : Enemy
{
    EnemyStates smallfry;

    public EnemyScriptable smallFriesData;
    void Start()
    {
        anim= GetComponent<Animator>();
        smallfry = EnemyStates.Idle;
        Spawn();

        HealthSystem healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDeath += HealthSystem_OnDeath;
        transform.LookAt(Castle);
    }

    private void HealthSystem_OnDeath(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyStates.Attack== smallfry)
        {
            anim.SetBool("Attacking", true);
            anim.SetBool("Running", false);
        }
        if (EnemyStates.Run == smallfry)
        {
            anim.SetBool("Running", true);
            anim.SetBool("Attacking", false);
        }

        Debug.Log(smallFriesData.health);
    }

    void Spawn()
    {
        agent.SetDestination(Castle.position);
        agent.speed = smallFriesData.speed;
    } 

    void Attack()
    {
        smallfry = EnemyStates.Attack;

    }




    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Traps"))
        {
            agent.speed = smallFriesData.speed / 1.4f;
            smallfry = EnemyStates.Run;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Traps"))   
        {
            Attack();
        }
        if (other.gameObject.CompareTag("Shield"))
        {
            gameObject.SetActive(false);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Castle"))
        {
            Attack();
        }
    }

  
}

public enum EnemyStates
{
    Idle,
    Attack,
    CastleAttack,
    Run,
    Death
}