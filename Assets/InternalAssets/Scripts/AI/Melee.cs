using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatSystem))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AnimationController))]
public class Melee : MonoBehaviour
{
    [SerializeField] protected Ray Ray;
    [SerializeField] private List<AttackPoint> attackPoints;
    private AudioSource audioSource;
    protected AnimationController animationController;
    protected StatSystem characterStats;
    public Vector3 raycastPoint;

    private bool canMove;
    private void Awake()
    {

    }
    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        DOTween.Kill(this);
    }

    protected virtual void Update()
    {
        raycastPoint = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        if (Physics.Raycast(raycastPoint, transform.forward, out RaycastHit hitInfo, characterStats.characterData.AttackRange))
        {

            if (hitInfo.transform.gameObject.layer != this.gameObject.layer)
            {
                animationController.Attack();
                if(!audioSource.isPlaying)
                {
                    audioSource.Play();

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
        else
        {
            
            if (characterStats.characterData.Movable)
            {
                animationController.ResetAnimation();
                animationController.Move();
                transform.position += (transform.forward * characterStats.characterData.Speed * Time.deltaTime);
            }
            else
            {
                animationController.ResetAnimation();
                animationController.Idle();
            }
        }

    }

    private void OnDeath(GameObject character)
    {
        animationController.ResetAnimation();
        animationController.Death();
        GameplayManager.Instance.WaveSystem.KilledInAction(character);
        transform.DOScale(0f, .25f);
        DOVirtual.DelayedCall(1f, () => Destroy(gameObject)).SetUpdate(false);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (characterStats != null)
            Debug.DrawRay(raycastPoint, transform.forward * characterStats.characterData.AttackRange, Color.green);
    }
}