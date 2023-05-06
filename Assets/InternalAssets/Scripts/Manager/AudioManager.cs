using UnityEngine.Audio;
using System;
using UnityEngine;
using Unity.VisualScripting;

//Credit to Brackeys youtube tutorial on Audio managers, as the majority of this code and learning how to use it was made by him.


public partial class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager Instance;
    //AudioManager

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
    }

    private void SpawnManager_OnHeroSpawn(bool championSpawn)
    {
        if(championSpawn)
        {
            Play("ChampionSpawn");
        }else
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
    public float volume;
    public bool loop;
}