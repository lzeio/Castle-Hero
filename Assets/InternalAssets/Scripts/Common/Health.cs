using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    
    private CharacterData _characterData;
    private int health;

    public event Action Death;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Init(CharacterData data)
    {
        health = data.Health;
    }

   
}

