using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class Melee : MonoBehaviour
{
    public CharacterData characterData;
    public Animator Animator;
    private bool canMove;
    Ray ray;

    protected virtual void Start()
    {
        Animator = GetComponent<Animator>();
        canMove = characterData.canMove;
    }

    protected virtual void Update()
    {
        if(Physics.Raycast(ray, out RaycastHit hitInfo,characterData.attackDistance))
        {
            Debug.Log(" checattack");
            if (hitInfo.collider.TryGetComponent(out Health health))
            {
                Debug.Log("attack");
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Debug.DrawRay(transform.position, transform.forward*characterData.attackDistance,Color.yellow);
    }
}
