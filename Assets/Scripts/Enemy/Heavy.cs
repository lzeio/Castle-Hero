using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Heavy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform Castle;

    public Animator anim;

    EnemyStates heavy;

    
    public EnemyScriptable heavyData;



    
    void Start()
    {
        anim = GetComponent<Animator>();
        heavy = EnemyStates.Idle;
        Spawn();
        HealthSystem healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDeath += HealthSystem_OnDeath;
        transform.LookAt(Castle);
    }

    private void HealthSystem_OnDeath(object sender, System.EventArgs e)
    {
        anim.SetBool("Dead", true);
        heavy = EnemyStates.Death;
        agent.Stop();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyStates.Attack == heavy)
        {
            anim.SetBool("HornAttack", true);
            anim.SetBool("Running", false);
        }
        if (EnemyStates.Run == heavy)
        {
            anim.SetBool("Running", true);
            anim.SetBool("HornAttack", false);
        }

        if (EnemyStates.CastleAttack == heavy)
        {
            anim.SetBool("Running", false);
            anim.SetBool("Attacking", true);
        }
        Debug.Log(heavyData.health);
    }

    void Spawn()
    {
        agent.SetDestination(Castle.position);
        agent.speed = heavyData.speed;
    }

    void TrapAttacks()
    {
        heavy = EnemyStates.Attack;
    }

    void CastleAttacks()
    {
        heavy = EnemyStates.CastleAttack;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Traps"))
        {
            GetComponent<HealthSystem>().Damage((int)other.GetComponent<Traps>().damage);
            agent.speed = heavyData.speed / 1.4f;
            heavy = EnemyStates.Run;
            other.GetComponent<Traps>().Damage(heavyData.attack);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Traps"))
        {
            TrapAttacks();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Called");
        if (collision.gameObject.CompareTag("Castle"))
        {
            agent.Stop();
            CastleAttacks();
            collision.gameObject.GetComponent<Castle>().Damage(heavyData.attack);
        }
    }

}
