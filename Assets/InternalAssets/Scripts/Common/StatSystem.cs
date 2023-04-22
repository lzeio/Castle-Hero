using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatSystem : MonoBehaviour
{
    public event Action OnDeath;
    public CharacterData characterData;
    private UpgradeableComponent upgradeableComponent;

    public int health;
    public int damage;

    public int NextUpgradeLevel;

    
    // Start is called before the first frame update
    void Start()
    {
        health = characterData.TierI_Health;
        damage = characterData.TierI_AttackDamage;
        try
        {
                upgradeableComponent = GetComponent<UpgradeableComponent>();
                upgradeableComponent.OnUpgrade += UpdateStats;
        }
        catch (Exception)
        {
            Debug.Log("Villains cannot be upgraded");
        }
        NextUpgradeLevel = 2;
    }

    private void UpdateStats()
    {
        switch (NextUpgradeLevel)
        {
            case 2:
                health = characterData.TierII_Health;
                damage = characterData.TierII_AttackDamage;
                NextUpgradeLevel++;
                break;
            case 3:
                health = characterData.TierIII_Health;
                damage = characterData.TierIII_AttackDamage;
                NextUpgradeLevel++;
                break;
            default:
                break;
        }
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
        return damage;
    }
}
