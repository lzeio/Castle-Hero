using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public partial class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager Instance;
    //AudioManager

    public AudioMixerGroup AudioMixer;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
        SpawnManager.OnHeroSpawn += SpawnManager_OnHeroSpawn;
        UIManager.OnButtonClicked += UIManager_OnButtonClicked;
        StatSystem.onDeath += StatSystem_onDeath;
    }

    private void StatSystem_onDeath(LayerMask Layer)
    {
        if (Layer == 6)
            Play("Death");
    }

    private void UIManager_OnButtonClicked()
    {
        Play("UI");
    }

    private void SpawnManager_OnHeroSpawn(bool championSpawn)
    {
        if (championSpawn)
        {
            Play("ChampionSpawn");
        }
        else
        {
            Play("HeroesSpawn");
        }
    }

    void Start()
    {
        Play("Theme");

    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.clipName == name);
        if (s == null || s.source.isPlaying)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        if(name == "Theme")
        {
            s.source.outputAudioMixerGroup = AudioMixer;
            //VVVVIP LINE
            AudioMixer.audioMixer.SetFloat("Volume", Mathf.Log10(.1f) * 20);
        }

        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.clipName == name);

        s.source.Stop();
    }
}

[System.Serializable]

public class Sound
{
    public AudioSource source;
    public AudioClip clip;
    public string clipName;
    [Range(0f,1f)]
    public float volume;
    public bool loop;
}