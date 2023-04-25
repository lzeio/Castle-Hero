using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUIScript : MonoBehaviour
{
    [SerializeField] private GameObject[] UIPanels;
    [SerializeField] private DOTweenAnimation[] GameUIAnimations;
    [SerializeField] private GameObject[] QualityHighlights;
    private int CurrentPanel;
    IEnumerator DelayedPanel(int index)
    {
        GameUIAnimations[0].DOPlayBackwardsAllById(CurrentPanel + "");
        yield return new WaitForSeconds(1.2f);
        ResetPanels();
        UIPanels[index].SetActive(true);
        GameUIAnimations[0].DORestartAllById(index + "");
    }
    private void ResetPanels()
    {
        foreach (GameObject panel in UIPanels)
        {
            panel.SetActive(false);
        }
    }
    public void SelectPanel(int index)
    {
        StartCoroutine(DelayedPanel(index));
        CurrentPanel = index;
        if(index == 1)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
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
        //PlayerPrefs.SetInt("QualityLevelIndex", index);
        GameData.SetCurrentQuality(index);

    }
}
