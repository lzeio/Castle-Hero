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

    public int MaxCastleHealth;
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

    }

    public void ResetGame()
    {
        WaveSystem.ResetWaveSystem();
        SpawnManager.ResetSpawnCharacter();
        UIManager.ResetHealthBarUI();
        CoinsManager.ResetCoins();
        CoinsManager.AddCoins(1000);
        FindObjectOfType<Castle>().UpdateCastleHealth(MaxCastleHealth);
        Time.timeScale = 0f;
    }
}
