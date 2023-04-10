using UnityEngine;

public class Infantry : Melee
{
   
    public void Attack()
    {
        animationController.Attack();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        //for testing // highly conditional 
        //raycastPoint = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1.5f);

    }
}
