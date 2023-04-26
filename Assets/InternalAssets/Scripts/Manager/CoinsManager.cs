using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public int Coins;

    public void AddCoins(int amount)
    {
        Coins += amount;
        GamePlayUIScript.Instance.CoinsText.GetComponent<TMP_Text>().text = Coins + "";
    }

    public bool HasEnoughCoins(int amount)
    {
        if (Coins >= amount)
            return true;
        return false;
    }
    
}
