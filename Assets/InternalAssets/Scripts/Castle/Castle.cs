using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Castle : MonoBehaviour
{
    public static event Action<int> OnCastleHealthUpdated;
    public int Health;
    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out StatSystem statSystem))
        {
            Health -= statSystem.health;
            statSystem.KillCharacter();
            OnCastleHealthUpdated?.Invoke(Health);
            Destroy(statSystem.gameObject);
        }
    }


}
