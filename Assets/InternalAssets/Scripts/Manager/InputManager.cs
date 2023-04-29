using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static Action OnSpawnHero;
    public static Action OnSpeedChange;
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
            OnSpawnHero?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            OnSpeedChange?.Invoke();
        }
        
    }

   
}
