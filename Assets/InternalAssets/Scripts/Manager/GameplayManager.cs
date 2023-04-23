using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TimeScaleOption
{
    Normal,
    Double,
    Quadruple
}
public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;


    public SpawnManager SpawnManager;
    public UIManager UIManager;
    public GridManagerVTwo GridManager_Two;
    public GridManager GridManager;
    public InputManager InputManager;
    public CoinsManager CoinsManager;

    private TimeScaleOption timeScale = TimeScaleOption.Normal;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        SpawnManager = FindObjectOfType<SpawnManager>();
        UIManager = FindObjectOfType<UIManager>();
        GridManager = FindObjectOfType<GridManager>();
        GridManager_Two = FindObjectOfType<GridManagerVTwo>();
        InputManager = FindObjectOfType<InputManager>();
        CoinsManager = FindObjectOfType<CoinsManager>();
    }

    public void ChangeTimeScale()
    {
        switch (timeScale)
        {
            case TimeScaleOption.Normal:
                timeScale = TimeScaleOption.Double;
                Time.timeScale = 2f;
                break;
            case TimeScaleOption.Double:
                timeScale = TimeScaleOption.Quadruple;
                Time.timeScale = 4f;
                break;
            case TimeScaleOption.Quadruple:
                timeScale = TimeScaleOption.Normal;
                Time.timeScale = 1f;
                break;
        }

        Debug.Log("Current Timescale Multiplier: " + timeScale.ToString());
    }
}
