using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{

    public enum WaveState
    {
        WAVE_INITIALIZING,
        WAVE_STARTED,
        WAVE_ENDED
    }

    private WaveState waveState;
    private void Awake()
    {
        waveState = WaveState.WAVE_INITIALIZING;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartWaves());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartWaves()
    {
        Debug.Log("Starting waves");
        waveState = WaveState.WAVE_STARTED;
        yield return new WaitForSeconds(3f);
        Debug.Log("Wave Started");
    }
}
