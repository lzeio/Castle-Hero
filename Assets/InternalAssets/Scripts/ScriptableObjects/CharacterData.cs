using UnityEngine;

[CreateAssetMenu(fileName = "CharacterName", menuName = "CharacterData")]
public class CharacterData : ScriptableObject
{

    public float speed;
    public float attackDamage;
    public float health;
    public bool canMove;
    public GameObject projectiles;
}
