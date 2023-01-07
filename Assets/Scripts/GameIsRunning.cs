using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIsRunning : MonoBehaviour
{
    public bool gameIsRunning = false;
    [SerializeField] GameObject pauseText;
    public bool started = false;

    public void ResumeGame()
    {
        gameIsRunning = true;
        pauseText.SetActive(false);
    }

    public void PauseGame()
    {
        gameIsRunning = false;
        pauseText.SetActive(true);
    }

    public void StartGame()
    {
        started = true;
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

        print("Started" + started);
        print("Is running" + gameIsRunning);
    }
}
