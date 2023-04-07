using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PulseAttack : MonoBehaviour
{
    public bool canPulse;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame && canPulse)
        {
            Debug.Log("Pulse Attacks"); 
        }
    }
}
