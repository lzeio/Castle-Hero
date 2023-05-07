using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{

    public static event Action OnButtonClicked;

    [SerializeField] private Image castleHealthBar;
    [SerializeField] private TMP_Text coinsCount;
    [SerializeField] private TMP_Text waveCount;
    [SerializeField] private TMP_Text TimeScale;

    [SerializeField] private List<Button> heroSelector = default;

    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject NewHighScorePopup;

    [Header("UIPanels")]
    [SerializeField] private GameObject[] GameUIPanels;
    [SerializeField] private GameObject[] MenuUIPanels;

    [Header("Settings")]
    [SerializeField] private GameObject[] SettingsQualityHighlights;
    [SerializeField] private GameObject[] PauseQualityHighlights;

    [SerializeField] private Slider[] MusicLevel;
    [SerializeField] private Slider[] SFXLevel;

    [Header("WaveHighScores")]
    [SerializeField] private TMP_Text EasyWaveText;
    [SerializeField] private TMP_Text MediumWaveText;
    [SerializeField] private TMP_Text HardWaveText;

    private void Start()
    {
        CoinsManager.OnCoinsUpdated += CoinsManager_OnCoinsUpdated;
        Castle.OnCastleHealthUpdated += Castle_OnCastleHealthUpdated;
        WaveSystem.OnWaveCountUpdated += WaveSystem_OnWaveCountUpdated;
        GameplayManager.OnTimeScaleChanged += GameplayManager_OnTimeScaleChanged;
        SetHighScores();
        SelectedQuality(GameData.GetCurrentQuality());
        SetMusicLevels(GameData.GetMusicLevel());
        SetSoundLevels(GameData.GetSoundLevel());
    }

    public void Update()
    {
        if (GameplayManager.Instance.SpawnManager.heroIndex == 5 && GameplayManager.Instance.SpawnManager.hasChampion)
        {
            return;
        }
        for (int i = 0; i < GameplayManager.Instance.SpawnManager.Heroes.Count; i++)
        {

            heroSelector[i].interactable = GameplayManager.Instance.CoinsManager.HasEnoughCoins(GameplayManager.Instance.SpawnManager.Heroes[i].Cost);
        }
    }
    private void GameplayManager_OnTimeScaleChanged(float time)
    {
        TimeScale.text = $"{time}X";
        //Time Audio here
    }

    private void WaveSystem_OnWaveCountUpdated(int count)
    {
        waveCount.text = $"WAVE : {count.ToString()}";
        //New Wave Audio here
    }

    private void Castle_OnCastleHealthUpdated(int health)
    {
        castleHealthBar.fillAmount = (float)health / 10000;
    }

    private void CoinsManager_OnCoinsUpdated()
    {
        coinsCount.text = GameplayManager.Instance.CoinsManager.Coins.ToString();
    }

    public void ExitGame()
    {
        Application.Quit();

    }


    public void Pause()
    {
        Time.timeScale = 0;
        SelectGamePanel(1);
        OnButtonClicked?.Invoke();
    }

    public void Continue()
    {
        SelectGamePanel(0);
        GameplayManager.Instance.ChangeTimeScale();
        OnButtonClicked?.Invoke();
    }

    public void DifficultySelection(int level)
    {
        SelectGamePanel(0);
        GameplayManager.Instance.ChangeTimeScale();
        OnButtonClicked?.Invoke();
        GameplayManager.Instance.WaveSystem.level = level;
    }
    private void ResetPanels()
    {
        foreach (GameObject Gpanel in GameUIPanels)
        {
            Gpanel.SetActive(false);
        }
        foreach (GameObject Mpanel in MenuUIPanels)
        {
            Mpanel.SetActive(false);
        }

    }
    public void SelectGamePanel(int index)
    {
        ResetPanels();
        GameUIPanels[index].SetActive(true);
        if(index == 1)
        {
            SetSoundLevels(GameData.GetSoundLevel());
            SetMusicLevels(GameData.GetMusicLevel());
        }
        OnButtonClicked?.Invoke();
    }
    public void SelectMenuPanel(int index)
    {
        ResetPanels();
        MenuUIPanels[index].SetActive(true);
        OnButtonClicked?.Invoke();
    }

    public void SelectedQuality(int index)
    {
        GameData.SetCurrentQuality(index);
        foreach  (GameObject Ghigh in SettingsQualityHighlights)
        {
            Ghigh.SetActive(false);
        }
        foreach  (GameObject Mhigh in PauseQualityHighlights)
        {
            Mhigh.SetActive(false);
        }
        SettingsQualityHighlights[index].SetActive(true) ;
        PauseQualityHighlights[index].SetActive(true) ;
        QualitySettings.SetQualityLevel(index);
    }

    private void SetHighScores()
    {
        EasyWaveText.text =  " "+GameData.GetEasyWave();
        MediumWaveText.text = " " + GameData.GetMediumWave();
        HardWaveText.text = " " + GameData.GetHardWave();
    }

    public void SetSoundLevels(float value)
    {
        for (int i = 0; i < SFXLevel.Length; i++)
        {
            SFXLevel[i].value = value;
        }
        GameData.SetSoundLevel(value);
        AudioManager.Instance.SFXLevels(value);
    }
    public void SetMusicLevels(float value)
    {
        for (int i = 0; i < MusicLevel.Length; i++)
        {
            MusicLevel[i].value = value;
        }
        GameData.SetMusicLevel(value);
        AudioManager.Instance.MusicLevels(value);
    }

}
