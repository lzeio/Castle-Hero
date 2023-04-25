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
    public int NextUpgradeCost;
    public int rowPosition, colPosition;
    
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
        NextUpgradeCost = characterData.TierII_Cost;
    }

    private void UpdateStats()
    {
        switch (NextUpgradeLevel)
        {
            case 2:
                health = characterData.TierII_Health;
                damage = characterData.TierII_AttackDamage;
                NextUpgradeLevel++;
                GameplayManager.Instance.CoinsManager.AddCoins(-NextUpgradeCost);
                NextUpgradeCost = characterData.TierIII_Cost;

                break;
            case 3:
                health = characterData.TierIII_Health;
                damage = characterData.TierIII_AttackDamage;
                GameplayManager.Instance.CoinsManager.AddCoins(-NextUpgradeCost);
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
            GameplayManager.Instance.GridManager_Two.Grid[rowPosition, colPosition].GetComponent<Tile>().IsOccupied = false;
        }

    }


    public int DealDamage()
    {
        return damage;
    }
}
