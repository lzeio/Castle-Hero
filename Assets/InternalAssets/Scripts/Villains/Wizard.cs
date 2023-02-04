using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wizard : Enemy
{
 
    public GameObject shield;
    public EnemyStates specialStates;


    public GameObject projectilePrefab;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }



    // Update is called once per frame
    void Update()
    {
        if(!shield.activeInHierarchy)
        {
            agent.stoppingDistance = 10f;
        }

        if(EnemyStates.Attack == specialStates)
        {
            anim.SetBool("Attacking",true);
        }

        Attack();   

    }

    void Attack()
    {
        if (agent.remainingDistance <=  30f  && agent.remainingDistance>=20f)
        {
            specialStates = EnemyStates.Attack;
            agent.isStopped = true;
        }
    
    }

    void Spawn()
    {
        agent.SetDestination(Castle.position);
    }

    
}
