using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] UIPanels;
    [SerializeField] private DOTweenAnimation[] MenuUIAnimations;
    [SerializeField] private GameObject[] QualityHighlights;
    private int CurrentPanel;
    public void Start()
    {
        SetQuality(GameData.GetCurrentQuality());
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    IEnumerator DelayedPanel(int index)
    {
        MenuUIAnimations[0].DOPlayBackwardsAllById(CurrentPanel + "");
        yield return new WaitForSeconds(1.2f);
        ResetPanels();
        UIPanels[index].SetActive(true);
        MenuUIAnimations[0].DORestartAllById(index + "");
    }
    public void OnMainMenu()
    {
        StartCoroutine(DelayedPanel(0));
        CurrentPanel = 0;
    }
    public void OnSettingsPanel()
    {
        StartCoroutine(DelayedPanel(1));
        CurrentPanel = 1;
    }
    public void OndifficultyPanel()
    {
        StartCoroutine(DelayedPanel(2));
        CurrentPanel= 2;
    }
    public void OnAlmanacPanel()
    {
        StartCoroutine(DelayedPanel(3));
        CurrentPanel= 3;
    }
    private void ResetPanels()
    {
        foreach (GameObject panel in UIPanels)
        { 
            panel.SetActive(false);
        }
    }
    public void SetQuality(int index)
    {
        foreach (GameObject highlight in QualityHighlights)
        {
            highlight.SetActive(false);
        }
        QualityHighlights[index].SetActive(true);
        QualitySettings.SetQualityLevel(index);
        GameData.SetCurrentQuality(index);
        
    }
    public void SelectDifficulty(int indedx)
    {
        Debug.Log("Selected" + indedx);
        SceneManager.LoadScene(1);
    }
}
