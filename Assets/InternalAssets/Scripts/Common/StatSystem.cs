using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatSystem : MonoBehaviour
{
    public event Action OnDeath;
    public CharacterData characterData;

    public int health; 
    // Start is called before the first frame update
    void Start()
    {
        health = characterData.Health;
    }

    public void UpdateHealth(int damage)
    {
        health -= damage;
        Debug.Log($"Health is {health} and Character is {this.gameObject}");
        if (health <= 0)
        {
            OnDeath?.Invoke();
        }

    }
    public int DealDamage()
    {
        return characterData.AttackDamage;
    }
}
