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
    public float AttackRange;
    public int Health;
    public bool CanMove;

    [Header("Character Abilities")]
    public GameObject Projectile= default;
}
