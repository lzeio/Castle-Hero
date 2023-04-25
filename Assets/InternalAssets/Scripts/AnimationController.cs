using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator Animator;
    private void Start()
    {
        Animator = GetComponent<Animator>();
    }
    public void Attack()
    {
        Animator.SetBool(AnimationConstants.CommonAnimation.Attacking, true);
    }
    public void Move()
    {
        Animator.SetBool(AnimationConstants.CommonAnimation.Walking, true);
    }

    public void Idle()
    {
        Animator.SetBool(AnimationConstants.CommonAnimation.Idle, true);
    }
    public void Death()
    {
        Animator.SetBool(AnimationConstants.CommonAnimation.Death, true);
    }

    public void ResetAnimation()
    {
        foreach (AnimatorControllerParameter parameter in Animator.parameters)
        {
            Animator.SetBool(parameter.name, false);
        }
    }

}
