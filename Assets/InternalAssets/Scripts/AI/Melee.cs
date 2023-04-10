using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(StatSystem))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AnimationController))]
public class Melee : MonoBehaviour
{
    [SerializeField] protected Ray Ray;
    [SerializeField] private List<AttackPoint> attackPoints;
    protected AnimationController animationController;
    protected StatSystem statSystem;
    public Vector3 raycastPoint;

    private bool canMove;
    private void Awake()
    {
       
    }
    protected virtual void Start()
    {
        animationController = GetComponent<AnimationController>();
        statSystem = GetComponent<StatSystem>();
        canMove = statSystem.characterData.CanMove;
        statSystem.OnDeath+= OnDeath;
        foreach(AttackPoint attackPoint in attackPoints)
        {
            attackPoint.SetStatsData(statSystem);
        }
    }

    private void OnDestroy()
    {
        statSystem.OnDeath -= OnDeath;
    }



    protected virtual void FixedUpdate()
    {
        raycastPoint = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + .5f);
        if (Physics.Raycast(raycastPoint, transform.forward, out RaycastHit hitInfo, statSystem.characterData.AttackRange))
        {

            if (hitInfo.transform.gameObject.layer != this.gameObject.layer)
            {
                Debug.Log(hitInfo.transform.name);
                animationController.Attack();
            }
            else
            {
                animationController.ResetAnimation();
                animationController.Idle();
            }
        }
        else 
        if (statSystem.characterData.CanMove)
        {
            animationController.ResetAnimation();
            animationController.Move();
            transform.position += (transform.forward * statSystem.characterData.Speed * Time.deltaTime);
        }
      
    }

    private void OnDeath()
    {
        animationController.ResetAnimation();
        animationController.Death();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if(statSystem!=null)
        Debug.DrawRay(raycastPoint, transform.forward * statSystem.characterData.AttackRange, Color.green);
    }
}