using System;
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
    public static event Action<float> OnTimeScaleChanged;
    public static GameplayManager Instance;


    public SpawnManager SpawnManager;
    public UIManager UIManager;
    public GridManagerVTwo GridManager_Two;
    public GridManager GridManager;
    public InputManager InputManager;
    public CoinsManager CoinsManager;
    public WaveSystem WaveSystem;
    public UpgradeSystem UpgradeSystem;
    private TimeScaleOption timeScale = TimeScaleOption.Normal;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        Time.timeScale = 0f;
    }
    private void Start()
    {
        SpawnManager = FindObjectOfType<SpawnManager>();
        UIManager = FindObjectOfType<UIManager>();
        GridManager = FindObjectOfType<GridManager>();
        GridManager_Two = FindObjectOfType<GridManagerVTwo>();
        InputManager = FindObjectOfType<InputManager>();
        CoinsManager = FindObjectOfType<CoinsManager>();
        WaveSystem = FindObjectOfType<WaveSystem>();
        UpgradeSystem = FindObjectOfType<UpgradeSystem>();
        InputManager.OnSpeedChange += ChangeTimeScale;
    }


    public void ChangeTimeScale()
    {
        switch (timeScale)
        {
            case TimeScaleOption.Normal:
                timeScale = TimeScaleOption.Double;
                Time.timeScale = 2f;
                OnTimeScaleChanged?.Invoke(Time.timeScale);
                break;
            case TimeScaleOption.Double:
                timeScale = TimeScaleOption.Quadruple;
                Time.timeScale = 4f;
                OnTimeScaleChanged?.Invoke(Time.timeScale);
                break;
            case TimeScaleOption.Quadruple:
                timeScale = TimeScaleOption.Normal;
                Time.timeScale = 1f;
                OnTimeScaleChanged?.Invoke(Time.timeScale);
                break;
        }

        Debug.Log("Current Timescale Multiplier: " + timeScale.ToString());
    }

    public void ResetGame()
    {
        WaveSystem.ResetWaveSystem();
        SpawnManager.ResetSpawnCharacter();
        FindObjectOfType<Castle>().Health = 5000;
        Time.timeScale = 0f;
    }
}
