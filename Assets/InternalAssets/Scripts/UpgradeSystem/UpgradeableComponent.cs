using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeableComponent : MonoBehaviour
{
    [SerializeField] private GameObject UI;
   [SerializeField] private Button upgradeButton;
   [SerializeField] private Button retireButton;
    // Start is called before the first frame update
    void Start()
    {
        upgradeButton.onClick.AddListener(Upgrade);
        retireButton.onClick.AddListener(Retire);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Upgrade()
    {
        Debug.Log("Upgrade");
    }

    public void Retire()
    {
        Debug.Log("Retire");
    }
    
    public  void ToggleUI(bool toggle)
    {
        UI.SetActive(toggle);
    }
}
