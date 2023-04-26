using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Castle : MonoBehaviour
{

    public int Health;
    public LayerMask villainLayerMask;
    public void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out StatSystem statSystem))
        {
            Health -= statSystem.health;
            other.transform.DOScale(0f, .2f).SetUpdate(false).OnComplete(() => Destroy(other.gameObject));
            GamePlayUIScript.Instance.CastleHealth.GetComponent<TMP_Text>().text = Health+"";
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }
}
