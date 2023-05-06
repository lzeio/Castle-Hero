using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{


    [SerializeField] private Image castleHealthBar;
    [SerializeField] private TMP_Text coinsCount;
    [SerializeField] private TMP_Text waveCount;
    [SerializeField] private TMP_Text TimeScale;

    [SerializeField] private List<Button> heroSelector = default;

    private void Start()
    {
        CoinsManager.OnCoinsUpdated += CoinsManager_OnCoinsUpdated;
        Castle.OnCastleHealthUpdated += Castle_OnCastleHealthUpdated;
        WaveSystem.OnWaveCountUpdated += WaveSystem_OnWaveCountUpdated;
        GameplayManager.OnTimeScaleChanged += GameplayManager_OnTimeScaleChanged;
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
    }

    private void Castle_OnCastleHealthUpdated(int health)
    {
        castleHealthBar.fillAmount = (float)health / 10000;
    }

    private void CoinsManager_OnCoinsUpdated()
    {
        coinsCount.text = GameplayManager.Instance.CoinsManager.Coins.ToString();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Continue()
    {
        GameplayManager.Instance.ChangeTimeScale();
    }


}
