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

    
}
