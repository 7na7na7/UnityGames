using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    static public WeatherManager instance;
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this; 
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private AudioManager TheAudio;
    public ParticleSystem rain;
    public string rain_sound;
    private void Start()
    {
        TheAudio = FindObjectOfType<AudioManager>();
        rain.Stop();
    }

    public void Rain()
    {
        TheAudio.Play(rain_sound);
        rain.Play();
    }

    public void RainStop()
    {
        TheAudio.Stop(rain_sound);
        rain.Stop();
    }

    public void RainDrop()
    {
        rain.Emit(10);
    }
}

