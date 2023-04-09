using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;
    // Start is called before the first frame update

    public SpawnManager SpawnManager;
    public UIManager UIManager;
    public GridManagerVTwo GridManager_Two;
    public GridManager GridManager;
    public InputManager InputManager;
    void Awake()
    {
        if (Instance == null)
        Instance = this;
    }
    private void Start()
    {
        SpawnManager = FindObjectOfType<SpawnManager>();
        UIManager=  FindObjectOfType<UIManager>();
        GridManager=  FindObjectOfType<GridManager>();
        GridManager_Two=  FindObjectOfType<GridManagerVTwo>();
        InputManager= FindObjectOfType<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
