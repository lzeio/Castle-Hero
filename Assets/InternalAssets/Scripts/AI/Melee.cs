using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Health))]
public class Melee : MonoBehaviour
{
    public CharacterData characterData;
    public Transform Raycaster;
    public Animator Animator;
    private bool canMove;
    public Ray Ray;

    protected virtual void Start()
    {
        Animator = GetComponent<Animator>();
        canMove = characterData.canMove;
    }

    protected virtual void FixedUpdate()
    {
        if (Physics.Raycast(Raycaster.position, transform.forward, out RaycastHit hitInfo, characterData.attackDistance))
        {
            if (hitInfo.collider.TryGetComponent(out Health health))
            {
                Attack();
            }
            else
            {
            //    Idle();
            }
        }
    }
    // Start is called before the first frame update

    public virtual void Attack()
    {
        Animator.SetBool(AnimationConstants.CommonAnimation.Attacking, true);
    }
    public virtual void Move()
    {
        Animator.SetBool(AnimationConstants.CommonAnimation.Walking, true);
    }

    public virtual void Idle()
    {

        Animator.SetBool(AnimationConstants.CommonAnimation.Walking, true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
       Debug.DrawRay(Raycaster.position, transform.forward * characterData.attackDistance, Color.yellow);
    }
}
