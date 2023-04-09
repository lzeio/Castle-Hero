using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatSystem : MonoBehaviour
{
    public event Action OnDeath;
    public CharacterData characterData;

    private int health; 
    // Start is called before the first frame update
    void Start()
    {
        health = characterData.Health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDeath?.Invoke();
        }
    }
    public int DealDamage()
    {
        return characterData.AttackDamage/10;
    }
}
