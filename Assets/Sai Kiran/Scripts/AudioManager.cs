using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

       foreach (Sound i in sounds)
        {
            i.source = gameObject.AddComponent<AudioSource>();
            i.source.clip = i.clip;

            i.source.volume = i.volume;
            i.source.pitch = i.pitch;

            i.source.loop = i.loop;
        }
    }

     void Start()
    {
        play("2");
    }

    public void play (string name)
    {
      Sound i = Array.Find(sounds, Sound => Sound.name == name);
        if (i == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
        i.source.Play();
    }

}
