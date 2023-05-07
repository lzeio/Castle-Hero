using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmanacManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLeftButtonClicked()
    {
        AudioManager.Instance.Play("TurnPage");
    }
    public void OnRightButtonClicked()
    {
        AudioManager.Instance.Play("TurnPage");
    }
}
