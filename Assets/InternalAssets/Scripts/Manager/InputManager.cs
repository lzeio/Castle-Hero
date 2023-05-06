using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static Action TrySpawnHero;
    public static Action OnSpeedChange;
    
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
        if (Input.GetMouseButtonUp(0))
        {
            TrySpawnHero?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            OnSpeedChange?.Invoke();
        }
        
    }

   
}
