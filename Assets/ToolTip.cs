using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string message;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        ToolTipManager.Instance.SetAndShowToolTip(message);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        ToolTipManager.Instance.HideToolTip();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
