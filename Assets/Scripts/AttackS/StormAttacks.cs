using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StormAttacks : MonoBehaviour
{


    [SerializeField] GameObject fireAttack;
    [SerializeField] GameObject freezeAttack;
    [SerializeField] Transform globe;
    [SerializeField] float attackRadius;
    Camera cam;
    public float timeBetweenAttacks;
    public bool isReadyToAttack;
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
            Physics.OverlapSphere(AttackPoint().point, attackRadius, LayerMask.GetMask(LayerMask.LayerToName(7)));
            Instantiate(fireAttack, AttackPoint().point, globe.transform.rotation, globe);
            Invoke("ResetAttack", timeBetweenAttacks);
        }
        else
        {
            return;
        }
    }
    public Collider[] col;
    private void FreezeAttack()
    {
        if (isReadyToAttack && AttackPoint().transform.gameObject.layer >= 6)
        {
            isReadyToAttack = false;
            col = Physics.OverlapSphere(AttackPoint().point, attackRadius, LayerMask.GetMask(LayerMask.LayerToName(7)));
            Instantiate(freezeAttack, AttackPoint().point, globe.transform.rotation, globe);
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
