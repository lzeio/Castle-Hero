using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Castle : MonoBehaviour
{
    public static event Action<int> OnCastleHealthUpdated;
    public static int Health;

    private void Start()
    {
        Health = GameplayManager.Instance.MaxCastleHealth;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out StatSystem statSystem))
        {
            Health -= statSystem.health;
            statSystem.KillCharacter();
            UpdateCastleHealth(Health);
            Destroy(statSystem.gameObject);
            GameplayManager.Instance.CoinsManager.AddCoins(50);
        }
    }

    public void UpdateCastleHealth(int amount)
    {
        Health = amount;
        OnCastleHealthUpdated?.Invoke(Health);
    }

}
