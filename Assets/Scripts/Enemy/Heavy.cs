using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Heavy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform Castle;

    public Animator anim;

    public EnemyStates heavy;

    
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
        if (EnemyStates.Run == heavy)
        {
            anim.SetBool("Running", true);
            anim.SetBool("HornAttack", false);
        }
        
        if (EnemyStates.Attack == heavy)
        {
            anim.SetBool("HornAttack", true);
            anim.SetBool("Running", false);
        }

        if (EnemyStates.CastleAttack == heavy)
        {
            anim.SetBool("Attacking", true);
            anim.SetBool("Running", false);
            heavy = EnemyStates.Death;
        }

        if (EnemyStates.Death == heavy)
        {
            anim.Play("Die");
        }
        Debug.Log(heavyData.health);
    }

    void Spawn()
    {
        agent.SetDestination(Castle.position);
        agent.speed = heavyData.speed;
    }

    void Attacks()
    {
        heavy = EnemyStates.Attack;
    }

    void CastleAttacks()
    {
        heavy = EnemyStates.CastleAttack;
    }

   public void OnDeath()
    {
        StartCoroutine(Death(3));
    }


    IEnumerator Death(int time)
    {
       yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Traps"))
        {
            GetComponent<HealthSystem>().DamageTaken((int)other.GetComponent<Traps>().damage);
            agent.speed = heavyData.speed / 1.4f;
            heavy = EnemyStates.Run;
            other.GetComponent<Traps>().DamageTaken(heavyData.attack);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Traps"))
        {
            Attacks();
        }
        if(other.gameObject.CompareTag("Shield"))
        {
            other.gameObject.GetComponent<Shield>().DamageTaken(heavyData.attack);
            GetComponent<HealthSystem>().DamageTaken((int)other.GetComponent<Shield>().damage);
            Attacks();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Castle"))
        {
            agent.Stop();
            CastleAttacks();
            collision.gameObject.GetComponent<Castle>().DamageTaken(heavyData.attack*2);
        }

    }

}
