using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatSystem))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Defenders : MonoBehaviour
{
    private AudioSource audioSource;
    protected AnimationController animationController;
    protected StatSystem statSystem;
    private bool canMove;
    

    private Vector3 raycastPoint;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animationController = GetComponent<AnimationController>();
        statSystem = GetComponent<StatSystem>();
        canMove = statSystem.characterData.Movable;
        statSystem.OnDeath += OnDeath;
    }

    // Update is called once per frame
    void Update()
    {
        if (statSystem.characterData.Movable)
        {
            animationController.Move();
            transform.position += (transform.forward * statSystem.characterData.Speed * Time.deltaTime);
        }

    }
    private void OnDeath(GameObject character)
    {
        if (animationController != null)
        {
            animationController.ResetAnimation();
            animationController.Death();
        }
        GameplayManager.Instance.WaveSystem.KilledInAction(character);
        transform.DOScale(0f, 1f);
        DOVirtual.DelayedCall(1f, () => Destroy(gameObject));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<StatSystem>(out StatSystem stats))
        {
            stats.UpdateHealth(statSystem.DealDamage());
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (statSystem != null)
            Debug.DrawRay(transform.position, transform.forward * statSystem.characterData.AttackRange, Color.red);
    }

}
