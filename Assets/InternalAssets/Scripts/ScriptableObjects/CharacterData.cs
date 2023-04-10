using UnityEngine;
using System;
using static UnityEditor.Progress;
using UnityEditor;

[CreateAssetMenu(fileName = "CharacterName", menuName = "CharacterData")]
public class CharacterData : ScriptableObject
{
    public string CharacterId;

    [Header("Character Stats")]
    public float Speed;
    public int AttackDamage;
    public int Health;
    public bool CanMove;
    public float AttackRange;

    [Header("Character Abilities")]
    public GameObject Projectile= default;
}
