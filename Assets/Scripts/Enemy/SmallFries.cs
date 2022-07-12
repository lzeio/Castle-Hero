using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallFries : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform Castle;

    public Animator anim;

    EnemyStates smallfry;

    public EnemyScriptable smallFriesData;
    void Start()
    {
        anim= GetComponent<Animator>();
        smallfry = EnemyStates.Idle;
        Spawn();
        transform.LookAt(Castle);
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
            GetComponent<HealthSystem>().Damage((int)other.GetComponent<Traps>().damage);
            agent.speed = smallFriesData.speed / 1.4f;
            smallfry = EnemyStates.Run;

            other.GetComponent<Traps>().Damage(smallFriesData.attack);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Traps"))
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