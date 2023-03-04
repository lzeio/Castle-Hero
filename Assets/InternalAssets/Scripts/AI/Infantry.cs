using UnityEngine;

public class Infantry : Melee
{

    public override void Attack()
    {
        base.Attack();
        Animator.SetBool(AnimationConstants.CommonAnimation.Attacking, true);

    }

    protected override void Start()
    {
        base.Start();
    }

}
