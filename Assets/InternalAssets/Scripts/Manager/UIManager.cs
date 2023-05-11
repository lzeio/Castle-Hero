using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEditor;
using System.Collections;
using System.Reflection;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static event Action OnButtonClicked;

    [SerializeField] private Image castleHealthBar;
    [SerializeField] private Image RedhealthBar;
    [SerializeField] private TMP_Text coinsCount;
    [SerializeField] private TMP_Text waveCount;
    [SerializeField] private TMP_Text TimeScale;

    [SerializeField] private List<Button> heroSelector = default;

    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject NewHighScorePopup;
    [SerializeField] private DOTweenAnimation NewWave;

    [Header("UIPanels")]
    [SerializeField] private GameObject[] GameUIPanels;
    [SerializeField] private GameObject[] MenuUIPanels;
    //[SerializeField] private DOTweenAnimation GameAnimation;
    [SerializeField] private DOTweenAnimation UIAnimation;
    [SerializeField] private int CurrentMenuAnim = 0;
    [SerializeField] private int CurrentGameAnim = 0;
    [SerializeField] private bool ButtonPressed = false;

    [Header("Settings")]
    [SerializeField] private GameObject[] SettingsQualityHighlights;
    [SerializeField] private GameObject[] PauseQualityHighlights;

    [SerializeField] private Slider[] MusicLevel;
    [SerializeField] private Slider[] SFXLevel;

    [Header("WaveHighScores")]
    [SerializeField] private TMP_Text EasyWaveText;
    [SerializeField] private TMP_Text MediumWaveText;
    [SerializeField] private TMP_Text HardWaveText;
    [SerializeField] private TMP_Text CurrentWave;
    [SerializeField] private GameObject HighScorePopup;
    [SerializeField] private TMP_Text FinalhighScore;
    [SerializeField] private int waveNo;
    [SerializeField] private int SelectedDifficulty;

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
        
    }

    private void WaveSystem_OnWaveCountUpdated(int count)
    {
        waveCount.text = $"WAVE : {count.ToString()}";
        NewWave.gameObject.SetActive(true);
        NewWave.DORestartAllById("NewWave");
        waveNo = count;
    }

    private void Castle_OnCastleHealthUpdated(int health)
    {
        castleHealthBar.fillAmount = (float)health / 5000;
        if(castleHealthBar.fillAmount < 0.20) 
        {
            castleHealthBar.gameObject.SetActive(false);
        }
        RedhealthBar.fillAmount = (float)health / 5000;
        if(health<= 0) 
        {
            SelectGamePanel(2);
            CurrentWave.text =  (waveNo - 1).ToString();
            Time.timeScale = 0;
            FinalhighScore.text = waveNo.ToString();
            if(SelectedDifficulty == 5)
            {
                if(GameData.GetEasyWave()<waveNo)
                {
                    GameData.SetEasyWave(waveNo - 1);
                    HighScorePopup.SetActive(true);
                }
                   

            }
            else if (SelectedDifficulty == 10)
            {
                if (GameData.GetEasyWave() < waveNo)
                {
                    GameData.SetMediumWave(waveNo - 1);
                    HighScorePopup.SetActive(true);
                }
                    
            }
            else if (SelectedDifficulty == 15)
            {
                if (GameData.GetEasyWave() < waveNo)
                {
                    GameData.SetMediumWave(waveNo - 1);
                    HighScorePopup.SetActive(true);
                }
                    
            }

        }

         
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
        SelectedDifficulty= level;
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
        if(ButtonPressed== false)
        {
            ButtonPressed =true;
            GameUIAnim(index);
            OnButtonClicked?.Invoke();
        }
        
    }
    public void SelectMenuPanel(int index)
    {
        if(ButtonPressed== false)
        {
            ButtonPressed =true;
            UIAnim(index);
            OnButtonClicked?.Invoke();
            if(index == 1)
            {
                SetHighScores();
            }
        }
       
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
        EasyWaveText.text =  "Wave "+GameData.GetEasyWave();
        MediumWaveText.text = "Wave " + GameData.GetMediumWave();
        HardWaveText.text = "Wave " + GameData.GetHardWave();
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
    void UIAnim(int index)
    {
        UIAnimation.DOPlayBackwardsAllById(CurrentMenuAnim + "");
        Debug.Log("Anim rewind");
        DOVirtual.DelayedCall(1f, () => DelayedAnim(index));
    }
    void GameUIAnim(int index)
    {
        //UIAnimation.DOPlayBackwardsAllById(CurrentGameAnim + "");
        DOVirtual.DelayedCall(1f, ()=> DelayedGameAnim(index));
    }
    void DelayedAnim( int index)
    {
        ResetPanels();
        MenuUIPanels[index].SetActive(true);
        UIAnimation.DORestartAllById(index + "");
        CurrentMenuAnim = index;
        ButtonPressed = false;
    }
    void DelayedGameAnim(int index )
    {
        ResetPanels();
        GameUIPanels[index].SetActive(true);
        if (index == 1)
        {
            SetSoundLevels(GameData.GetSoundLevel());
            SetMusicLevels(GameData.GetMusicLevel());
            AudioManager.Instance.SFXLevels(0f);
            GameplayManager.Instance.SpawnManager.DisableSpawning();

        }
        CurrentGameAnim = index;
        UIAnimation.DORestartAllById(index + "");
        ButtonPressed = false;
    }
    public void ReloadScene()
    {
        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload the current scene
        SceneManager.LoadScene(currentScene.name);
    }
}
