using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static Action OnClick;
    public static InputManager Instance { get; private set; }
    public int heroindex;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
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
        SelectHero();
    }

    public void SelectHero()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            heroindex = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            heroindex = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            heroindex = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            heroindex =3;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            heroindex=4;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            heroindex = 5;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            heroindex = 6;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            heroindex =7;   
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            heroindex = 8;  
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            heroindex = 9;
        }
    }
}
