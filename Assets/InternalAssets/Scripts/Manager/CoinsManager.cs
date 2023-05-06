using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public static event Action OnCoinsUpdated;
    public int Coins;

    public void AddCoins(int amount)
    {
        Coins += amount;
        OnCoinsUpdated?.Invoke();
      
    }

    public bool HasEnoughCoins(int amount)
    {
        if (Coins >= amount)
            return true;
        return false;
    }
    
}
