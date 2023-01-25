using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "Enemy")]
public class EnemyScriptable : ScriptableObject
{
    public float speed;
    public float attack;
    public float health;
    public GameObject projectiles;
}
