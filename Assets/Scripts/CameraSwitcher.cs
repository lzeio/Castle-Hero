using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{

    public CinemachineVirtualCamera topDown;
    public CinemachineVirtualCamera castleCam;
    // Start is called before the first frame update
    void Start()
    {
        topDown.enabled = true;
        castleCam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Keyboard.current.digit1Key.isPressed))
        {
            topDown.enabled = false;
            castleCam.enabled = true;
        }
        if ((Keyboard.current.digit2Key.isPressed))
        {
            topDown.enabled = true;
            castleCam.enabled = false;
        }
    }
}

