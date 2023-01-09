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
        StartCoroutine(FadeIt2(normalMusicClip, 2, 10));

    }

    public void SetClipFinalMusic()
    {
        AudioSource[] sources = GetComponents<AudioSource>();

        AudioSource newClip = gameObject.AddComponent<AudioSource>();
        newClip.clip = finalMusicClip;
        newClip.volume = audioSource.volume;
        newClip.loop = true;

        foreach (AudioSource source in sources)
        {
            Destroy(source);
        }
        //newClip.outputAudioMixerGroup = audioSource.outputAudioMixerGroup;
        newClip.Play();
        //StartCoroutine(FadeIt2(finalMusicClip, 2, 2));
    }

    public void PauseMusic()
    {
        audioSource.Pause();
    }

    public void ResumeMusic()
    {
        audioSource.UnPause();
    }
    IEnumerator FadeIt2(AudioClip clip, float volume, float timeToFade)
    {
        AudioSource[] sources = GetComponents<AudioSource>();

        float time = audioSource.time;

        AudioSource newClip = gameObject.AddComponent<AudioSource>();
        newClip.clip = clip;
        newClip.volume = 0;
        newClip.loop = true;
        //newClip.outputAudioMixerGroup = audioSource.outputAudioMixerGroup;
        newClip.Play();
        
        float timePassed = 0;
        float newVolume = audioSource.volume;

        audioSource.time = time;

        while (timePassed < timeToFade)
        {
            timePassed += Time.deltaTime;
            newClip.volume = Mathf.Lerp(0f, newVolume, timePassed / timeToFade);
            audioSource.volume = Mathf.Lerp(newVolume, 0f, timePassed / timeToFade);
            yield return null;
        }
        newClip.volume = volume;
        audioSource.volume = 0;
        audioSource.Stop();
        //destroy the fading audiosource
        audioSource.enabled = false;
        audioSource = newClip;

        foreach (AudioSource source in sources)
        {
            Destroy(source);
        }


        yield break;
    }

}
