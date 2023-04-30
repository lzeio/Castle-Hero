using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Castle : MonoBehaviour
{

    public int Health;
    public void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out StatSystem statSystem))
        {
                Health -= statSystem.health;
                statSystem.KillCharacter();
                // GamePlayUIScript.Instance.CastleHealth.GetComponent<TMP_Text>().text = Health+"";
                Destroy(statSystem.gameObject);
        }
    }

  
}
