using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    static public BGMManager instance;
    public float volumn = 1f;
    public AudioClip[] clips; //배경음악들
    public AudioSource source;
    private WaitForSeconds waitTime=new WaitForSeconds(0.01f); //new를 한번만 실행해 성능상승
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
    }
    public void Play(int track)
    {
        source.volume = volumn;
        source.clip = clips[track];
        source.Play();
    }

    public void SetVolumn(float _volume)
    {
        source.volume = _volume;
    }
    public void Pause()
    {
        source.Pause();
    }
    public void UnPause()
    {
        source.UnPause();
    }
    public void Stop()
    {
        source.Stop();
    }
    public void FadeOutMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutMusicCoroutine());
    }

    IEnumerator FadeOutMusicCoroutine()
    {
        for (float i = 1.0f; i >= 0f; i -= 0.01f)
        {
            source.volume = i;
            yield return waitTime;
        }
    }
    public void FadeInMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInMusicCoroutine());
    }
    IEnumerator FadeInMusicCoroutine()
    {
        for (float i = 0f; i <= 1f; i += 0.01f)
        {
            source.volume = i;
            yield return waitTime;
        }
    }
}
