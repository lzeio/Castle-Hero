using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IamChampion : MonoBehaviour
{
    public  static IamChampion Instance;
    public static event Action OnChampionIsSlain;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        OnChampionIsSlain?.Invoke();
    }
}
