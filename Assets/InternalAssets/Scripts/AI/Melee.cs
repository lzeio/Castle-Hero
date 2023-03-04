using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class Melee : MonoBehaviour
{
    public CharacterData characterData;
    public Animator Animator;
    private bool canMove;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        Animator = GetComponent<Animator>();
        canMove = characterData.canMove;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Attack()
    {
        Animator.SetBool(AnimationConstants.CommonAnimation.Attacking, true);
    }
    public virtual void Move()
    {
        Animator.SetBool(AnimationConstants.CommonAnimation.Walking, true);
    }
}
