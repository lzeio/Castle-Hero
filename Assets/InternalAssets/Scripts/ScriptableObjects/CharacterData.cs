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
    public float AttackRange;

    [Header("Tier 1 Upgrades")]
    public int TierI_AttackDamage;
    public int TierI_Health;

    [Header("Tier 2 Upgrades")]
    public int TierII_AttackDamage;
    public int TierII_Health; 

    [Header("Tier 3 Upgrades")]
    public int TierIII_AttackDamage;
    public int TierIII_Health;

    [Header("Character Abilities")]
    public bool Movable;
    public GameObject Projectile= default;
}
