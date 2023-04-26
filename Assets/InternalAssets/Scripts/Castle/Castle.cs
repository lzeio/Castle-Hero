using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{

    public int Health;
    public LayerMask villainLayerMask;
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        Health -= other.GetComponent<StatSystem>().health;
        other.transform.DOScale(0f, .1f).SetUpdate(false).OnComplete(() => Destroy(other.gameObject));
    }

    private void OnTriggerStay(Collider other)
    {

    }
}
