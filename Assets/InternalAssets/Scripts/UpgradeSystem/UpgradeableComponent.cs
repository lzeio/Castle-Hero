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
    // Start is called before the first frame update
    void Start()
    {
        upgradeButton.onClick.AddListener(Upgrade);
        retireButton.onClick.AddListener(Retire);
        characterData = GetComponent<StatSystem>().characterData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Upgrade()
    {
        OnUpgrade?.Invoke();    
    }

    public void Retire()
    {
        Destroy(gameObject);
    }
    
    public  void ToggleUI(bool toggle)
    {
        UI.SetActive(toggle);
    }
}
