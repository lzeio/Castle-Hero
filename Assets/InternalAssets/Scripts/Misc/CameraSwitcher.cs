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
        topDown.enabled = false;
        castleCam.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.cKey.isPressed)
        {
            if (topDown.enabled)
            {
                topDown.enabled = false;
                castleCam.enabled = true;
            }
            else
            {
                topDown.enabled = true;
                castleCam.enabled = false;
            }
        }
    }
}

