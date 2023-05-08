using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StatSystem : MonoBehaviour
{
    public event Action<GameObject> OnDeath;
    public static event Action<LayerMask> onDeath;
    public CharacterData characterData;

    
    private UpgradeableComponent upgradeableComponent;

    public int health;
    public int damage;

    public int NextUpgradeLevel;
    public int NextUpgradeCost;
    public int rowPosition = -1, colPosition = -1;

    // Start is called before the first frame update
    void Start()
    {
        health = characterData.Base_Health;
        damage = characterData.Base_Damage;
        try
        {
            upgradeableComponent = GetComponent<UpgradeableComponent>();
            upgradeableComponent.OnUpgrade += UpdateStats;
        }
        catch (Exception)
        {
            Debug.Log("Villains cannot be upgraded");
        }
        NextUpgradeLevel = 1;
        NextUpgradeCost = characterData.TierI_Cost;
    }

    private void UpdateStats()
    {
        switch (NextUpgradeLevel)
        {
            case 1:
                health = characterData.TierI_Health;
                damage = characterData.TierI_AttackDamage;
                NextUpgradeLevel++;
                GameplayManager.Instance.CoinsManager.AddCoins(-NextUpgradeCost);
                GameplayManager.Instance.UpgradeSystem._upgradeable.TierLevel.sprite=GameplayManager.Instance.UpgradeSystem.TierI;
                NextUpgradeCost = characterData.TierII_Cost;
                break;      
            case 2:
                health = characterData.TierII_Health;
                damage = characterData.TierII_AttackDamage;
                NextUpgradeLevel++;
                GameplayManager.Instance.CoinsManager.AddCoins(-NextUpgradeCost);
                GameplayManager.Instance.UpgradeSystem._upgradeable.TierLevel.sprite = GameplayManager.Instance.UpgradeSystem.TierI2;
                NextUpgradeCost = characterData.TierII_Cost;

                break;
            case 3:
                health = characterData.TierIII_Health;
                damage = characterData.TierIII_AttackDamage;
                GameplayManager.Instance.CoinsManager.AddCoins(-NextUpgradeCost);
                GameplayManager.Instance.UpgradeSystem._upgradeable.TierLevel.sprite = GameplayManager.Instance.UpgradeSystem.TierI3;
                NextUpgradeCost = int.MaxValue;
                break;
            default:
                break;
        }
        AudioManager.Instance.Play("Upgrade");
    }

    bool isAlive = true;
    public void UpdateHealth(int damage)
    {
        health -= damage;
        if (health <= 0 && isAlive)
        {
            isAlive = false;
            OnDeath?.Invoke(gameObject);
            GameplayManager.Instance.CoinsManager.AddCoins(characterData.Reward);
            GameplayManager.Instance.GridManager_Two.Grid[rowPosition, colPosition].GetComponent<Tile>().IsOccupied = false;
            GameplayManager.Instance.SpawnManager.SpawnHeroes.Remove(this.gameObject);
            GameplayManager.Instance.WaveSystem.EnemiesGameObject.Remove(this.gameObject);  
            onDeath(gameObject.layer);
        }

    }

    public void KillCharacter()
    {
        health = 0;
        OnDeath?.Invoke(gameObject);
        GameplayManager.Instance.GridManager_Two.Grid[rowPosition, colPosition].GetComponent<Tile>().IsOccupied = false;
    }

    public int DealDamage()
    {
        return damage;
    }
    public void DealDamageOverTime(int damage, int duration)
    {
        StartCoroutine(DealsDamageOverTime(damage, duration));
    }
    public IEnumerator DealsDamageOverTime(int damage, int duration)
    {
        for (int i = 0; i < duration; i++)
        {
            UpdateHealth(damage);
            yield return new WaitForSeconds(1);
        }
    }
}
