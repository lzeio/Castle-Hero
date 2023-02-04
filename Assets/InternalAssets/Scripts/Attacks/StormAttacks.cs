using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class StormAttacks : MonoBehaviour
{


    [SerializeField] GameObject fireAttack;
    [SerializeField] GameObject freezeAttack;
    [SerializeField] Transform globe;
    [SerializeField] float attackRadius;
    Camera cam;
    public float timeBetweenAttacks;
    public bool isReadyToAttack;
    public Collider[] onFire;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.qKey.wasPressedThisFrame)
        {
            FireAttack();
        }
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
           FreezeAttack();
        }
    }

    private void FireAttack()
    {

        if (isReadyToAttack && AttackPoint().transform.gameObject.layer == 6)
        {
            isReadyToAttack = false;
            Instantiate(fireAttack, AttackPoint().point, globe.transform.rotation, globe);
            onFire = Physics.OverlapSphere(AttackPoint().point, attackRadius, LayerMask.GetMask(LayerMask.LayerToName(7)));
            foreach (Collider enemy in onFire)
            {
                if(enemy.gameObject.layer==7)
                {
                    enemy.GetComponent<HealthSystem>().FireDamage(5);
                }
            }
            Invoke("ResetAttack", timeBetweenAttacks);
        }
        else
        {
            return;
        }
    }
    public Collider[] frozen;
    private void FreezeAttack()
    {
        if (isReadyToAttack && AttackPoint().transform.gameObject.layer >= 6)
        {
            isReadyToAttack = false;
            Instantiate(freezeAttack, AttackPoint().point, globe.transform.rotation, globe);
            frozen = Physics.OverlapSphere(AttackPoint().point, attackRadius, LayerMask.GetMask(LayerMask.LayerToName(7)));
            foreach(Collider enemy in frozen)
            {
                if(enemy.gameObject.layer== 7)
                {
                    enemy.GetComponent<HealthSystem>().DamageTaken(10);
                    enemy.GetComponent<NavMeshAgent>().speed /= 0.5f;
                }
            }
            Invoke("ResetAttack", timeBetweenAttacks);
        }
        else
        {
            return;
        }

    }

  

    void ResetAttack()
    {
        isReadyToAttack = true ;
       
    }

    RaycastHit hit;
    RaycastHit AttackPoint()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out hit))
        {
                return hit;
        }
        return hit;
    }

}
