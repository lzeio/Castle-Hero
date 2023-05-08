using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTipManager : MonoBehaviour
{
    public static ToolTipManager Instance;

    public GameObject Tooltip;
    public TextMeshProUGUI textComponenet;
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(Instance);
        else
            Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Tooltip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Tooltip.transform.position = Input.mousePosition;
    }

    public void SetAndShowToolTip(string message)
    {
        Tooltip.SetActive(true);
        textComponenet.text = message;
    }

    public void HideToolTip()
    {
        Tooltip.SetActive(false);
    }
}
