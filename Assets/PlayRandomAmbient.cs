using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomAmbient : MonoBehaviour
{
    float timeLeftToWait = 0f;
    [SerializeField] float minWaitTime = 10f;
    [SerializeField] float maxWaitTime = 30f;
    AudioSource soundToPlay;

    private void Start()
    {
        soundToPlay = GetComponent<AudioSource>();
        timeLeftToWait = Random.Range(minWaitTime, maxWaitTime);
    }


    // Update is called once per frame
    void Update()
    {
        timeLeftToWait -= Time.deltaTime;
        if (timeLeftToWait <= 0f)
        {
            timeLeftToWait = Random.Range(minWaitTime, maxWaitTime) + soundToPlay.clip.length;
            soundToPlay.Play();
            print(soundToPlay.clip.name + " played!");
        }
    }
}
