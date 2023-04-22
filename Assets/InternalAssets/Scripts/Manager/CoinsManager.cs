using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    private int Coins;

    public void AddCoins(int amount)
    {
        Coins += amount;
    }

    public bool HasEnoughCoins(int amount)
    {
        if (Coins >= amount)
            return true;
        return false;
    }
    
}
