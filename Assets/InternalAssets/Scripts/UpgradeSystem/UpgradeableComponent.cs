using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeableComponent : MonoBehaviour
{
    public event Action OnUpgrade;
   [SerializeField] private GameObject UI;
   [SerializeField] private Button upgradeButton;
   [SerializeField] private Button retireButton;

    private CharacterData characterData;
    private StatSystem statSystem;
    // Start is called before the first frame update
    void Start()
    {
        upgradeButton.onClick.AddListener(Upgrade);
        retireButton.onClick.AddListener(Retire);
        statSystem = GetComponent<StatSystem>();
        characterData = statSystem.characterData;
    }

    // Update is called once per frame
    void Update()
    {
        upgradeButton.interactable = GameplayManager.Instance.CoinsManager.HasEnoughCoins(statSystem.NextUpgradeCost);  
    }

    public void Upgrade()
    {
        OnUpgrade?.Invoke();    
    }

    public void Retire()
    {
        GameplayManager.Instance.GridManager_Two.Grid[statSystem.rowPosition, statSystem.colPosition].GetComponent<Tile>().IsOccupied = false;
        Destroy(gameObject);
    }
    
    public  void ToggleUI(bool toggle)
    {
        UI.SetActive(toggle);
    }
}
