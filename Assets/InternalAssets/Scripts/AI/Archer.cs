using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Shooter
{
    private List<GameObject> arrowList;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }


    private void SpawnArrow()
    {
        GameObject arrow = Instantiate(characterStats.characterData.Projectile, transform.GetChild(2).transform.position, Quaternion.identity);
        AttackPoint arrowAttack = arrow.GetComponent<AttackPoint>();
        arrowAttack.ArrowAttack(characterStats);
        arrow.transform.SetParent(transform.GetChild(2).transform, false);
        arrow.transform.parent = null;
        arrow.transform.localScale = characterStats.characterData.Projectile.transform.localScale;
        ShootProjectile(arrow);
    }

    
    public GameObject ArrowPool()
    {
        foreach(var arrow in arrowList)
        {
            if (!arrow.activeInHierarchy)
                return arrow;
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (statSystem != null)
            Debug.DrawRay(raycastPoint, transform.forward * statSystem.characterData.AttackRange, Color.yellow);
    }

}
