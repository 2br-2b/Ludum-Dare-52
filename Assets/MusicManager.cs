using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip normalMusicClip;
    [SerializeField] AudioClip finalMusicClip;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SwitchClipToMain()
    {
        //audioSource.clip = normalMusicClip;
        audioSource.Play();
        StartCoroutine(FadeIt2(normalMusicClip, 2));

    }

    public void SetClipFinalMusic()
    {
        //audioSource.clip = finalMusicClip;
        audioSource.Play();
        StartCoroutine(FadeIt2(normalMusicClip, 2));
    }

    public void PauseMusic()
    {
        audioSource.Pause();
    }

    public void ResumeMusic()
    {
        audioSource.UnPause();
    }

    IEnumerator FadeIt(AudioClip clip, float volume)
    {

        ///Add new audiosource and set it to all parameters of original audiosource
        AudioSource fadeOutSource = gameObject.AddComponent<AudioSource>();
        fadeOutSource.clip = audioSource.clip;
        fadeOutSource.time = audioSource.time;
        fadeOutSource.volume = audioSource.volume;
        fadeOutSource.outputAudioMixerGroup = audioSource.outputAudioMixerGroup;
        fadeOutSource.time = audioSource.time;

        //make it start playing
        fadeOutSource.Play();

        //set original audiosource volume and clip
        audioSource.volume = 0f;
        audioSource.clip = clip;
        float t = 0;
        float v = fadeOutSource.volume;
        audioSource.Play();

        //begin fading in original audiosource with new clip as we fade out new audiosource with old clip
        while (t < 0.98f)
        {
            t = Mathf.Lerp(t, 1f, Time.deltaTime * 0.2f);
            fadeOutSource.volume = Mathf.Lerp(v, 0f, t);
            audioSource.volume = Mathf.Lerp(0f, volume, t);
            yield return null;
        }
        audioSource.volume = volume;
        //destroy the fading audiosource
        Destroy(fadeOutSource);
        yield break;
    }
    IEnumerator FadeIt2(AudioClip clip, float volume)
    {
        float time = audioSource.time;

        AudioSource newClip = gameObject.AddComponent<AudioSource>();
        newClip.clip = clip;
        newClip.volume = 0;
        //newClip.outputAudioMixerGroup = audioSource.outputAudioMixerGroup;
        newClip.Play();
        
        float timePassed = 0;
        float newVolume = audioSource.volume;

        float timeToFade = 10f;

        audioSource.time = time;

        while (timePassed < timeToFade)
        {
            timePassed += Time.deltaTime;
            newClip.volume = Mathf.Lerp(0f, newVolume, timePassed / timeToFade);
            audioSource.volume = Mathf.Lerp(newVolume, 0f, timePassed / timeToFade);
            yield return null;
        }
        newClip.volume = volume;
        //destroy the fading audiosource
        Destroy(audioSource);
        audioSource = newClip;
        yield break;
    }

}
