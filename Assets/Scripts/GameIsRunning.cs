using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIsRunning : MonoBehaviour
{
    public bool gameIsRunning = false;
    [SerializeField] GameObject pauseText;
    public bool started = false;
    float timeRunning = 0f;

    [SerializeField] GameObject musicManagerGameObject;
    MusicManager musicManager;
    
    public void Start()
    {
        musicManager = musicManagerGameObject.GetComponent<MusicManager>();
    }

    public void ResumeGame()
    {
        gameIsRunning = true;
        pauseText.SetActive(false);
        musicManager.ResumeMusic();
    }

    public void PauseGame()
    {
        gameIsRunning = false;
        pauseText.SetActive(true);
        musicManager.PauseMusic();
    }

    public void StopGame()
    {
        gameIsRunning = false;
        pauseText.SetActive(false);
        musicManager.SetClipFinalMusic();
    }

    public void StartGame()
    {
        started = true;
        musicManager.SwitchClipToMain();
        ResumeGame();
    }

    void Update()
    {
        if (started && Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsRunning)
                PauseGame();
            else
                ResumeGame();
        }

        if (!started)
        {
            gameIsRunning = false;
        }

        if (gameIsRunning)
        {
            timeRunning += Time.deltaTime;
        }
    }

    public float getTimeRunning()
    {
        return timeRunning;
    }
}
