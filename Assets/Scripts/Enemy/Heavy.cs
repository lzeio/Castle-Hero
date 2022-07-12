using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Heavy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform Castle;

    public Animator anim;

    EnemyStates smallfry;

    public EnemyScriptable heavyData;
    void Start()
    {
        anim = GetComponent<Animator>();
        smallfry = EnemyStates.Idle;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyStates.Attack == smallfry)
        {
            anim.SetBool("Attacking", true);
            anim.SetBool("Running", false);
        }
        if (EnemyStates.Run == smallfry)
        {
            anim.SetBool("Running", true);
            anim.SetBool("Attacking", false);
        }

        Debug.Log(heavyData.health);
    }

    void Spawn()
    {
        agent.SetDestination(Castle.position);
        agent.speed = heavyData.speed;
    }

    void Attack()
    {
        smallfry = EnemyStates.Attack;

    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Traps")
        {
            GetComponent<HealthSystem>().Damage((int)other.GetComponent<Traps>().damage);
            agent.speed = heavyData.speed / 1.4f;
            smallfry = EnemyStates.Run;

            other.GetComponent<Traps>().Damage(heavyData.attack);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Traps")
        {
            Attack();
        }
    }
}
