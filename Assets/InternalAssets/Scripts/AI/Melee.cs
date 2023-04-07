using System;
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
    protected AnimationController animationController;
    protected StatSystem statSystem;
    protected Vector3 raycastPoint;
    
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
            if (hitInfo.collider.TryGetComponent(out StatSystem statSystem))
            {
                animationController.Attack();
            }
        }
        else
        {
            animationController.ResetAnimation();
            animationController.Idle();
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
        Debug.DrawRay(raycastPoint, transform.forward * statSystem.characterData.AttackRange, Color.yellow);
    }
}