using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DaysLeftTimerTextUpdate : MonoBehaviour
{
    [SerializeField] GameObject gameRunningManagerObject;
    GameIsRunning gameRunningManager;

    [SerializeField] GameObject gameOverManagerObject;
    GameOverScript gameOverManager;

    TextMeshProUGUI textMesh;
    bool hasBeenShown = false;

    // Start is called before the first frame update
    void Start()
    {
        gameRunningManager = gameRunningManagerObject.GetComponent<GameIsRunning>();
        gameOverManager = gameOverManagerObject.GetComponent<GameOverScript>();
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasBeenShown && gameRunningManager.started)
        {
            textMesh.enabled = true;
        }
        
        int days_left  = ((int)gameOverManager.GetTimeLeft()) / 60 + 1;

        if (days_left > 10)
        {
            textMesh.text = "Unlimited Mode: Day " + (int)(gameRunningManager.getTimeRunning() / 60 + 1);
        }
        else
        {
            textMesh.text = days_left + " days until Christmas!";
        }
        

    }
}
