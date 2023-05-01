using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyCharacter : MonoBehaviour
{
    private Button _button;

    [SerializeField] private CharacterData characterData;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        _button.interactable = GameplayManager.Instance.CoinsManager.HasEnoughCoins(characterData.Cost);
    }
}
