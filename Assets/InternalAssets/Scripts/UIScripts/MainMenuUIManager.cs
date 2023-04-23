using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] UIPanels;
    [SerializeField] private DOTweenAnimation[] MenuUIAnimations;
    [SerializeField] private GameObject[] QualityHighlights;
    private int CurrentPanel;
    public void Start()
    {
        SetQuality(PlayerPrefs.GetInt("QualityLevelIndex", 2));
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
        PlayerPrefs.SetInt("QualityLevelIndex",index);
        
    }
}
