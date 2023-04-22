using DG.Tweening;
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
    protected StatSystem characterStats;
    public Vector3 raycastPoint;

    private bool canMove;
    private void Awake()
    {

    }
    protected virtual void Start()
    {
        animationController = GetComponent<AnimationController>();
        characterStats = GetComponent<StatSystem>();
        canMove = characterStats.characterData.Movable;
        characterStats.OnDeath += OnDeath;
        foreach (AttackPoint attackPoint in attackPoints)
        {
            attackPoint.SetStatsData(characterStats);
        }
    }

    private void OnDestroy()
    {
        characterStats.OnDeath -= OnDeath;
    }



    protected virtual void FixedUpdate()
    {
        raycastPoint = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        if (Physics.Raycast(raycastPoint, transform.forward, out RaycastHit hitInfo, characterStats.characterData.AttackRange))
        {

            if (hitInfo.transform.gameObject.layer != this.gameObject.layer)
            {
                animationController.Attack();
            }
            else
            {
                animationController.ResetAnimation();
                animationController.Idle();
            }
        }
        else
        {
            if (characterStats.characterData.Movable)
            {
                animationController.ResetAnimation();
                animationController.Move();
                transform.position += (transform.forward * characterStats.characterData.Speed * Time.deltaTime);
            }
        }

    }

    private void OnDeath()
    {
        animationController.ResetAnimation();
        animationController.Death();
        transform.DOScaleY(0, 1f).OnComplete(()=> Destroy(gameObject));
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (characterStats != null)
            Debug.DrawRay(raycastPoint, transform.forward * characterStats.characterData.AttackRange, Color.green);
    }
}