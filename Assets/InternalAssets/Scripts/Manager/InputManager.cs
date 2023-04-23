using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static Action OnClick;
    public int heroindex;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick?.Invoke();
        }
        
    }

    public void SelectHero(int index)
    {
        heroindex = index;
    }
}
