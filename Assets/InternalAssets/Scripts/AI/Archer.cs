using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Shooter
{
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
    }

}
