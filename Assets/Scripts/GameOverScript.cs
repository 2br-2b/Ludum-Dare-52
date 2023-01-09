using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    // Serialize an amount of time
    [SerializeField] float gameLengthInSeconds = 5f * 60f;

    [SerializeField] GameObject conversationHolder;
    NPCConversation conversation;

    [SerializeField] GameObject gameRunningManagerHolder;
    GameIsRunning gameRunningManager;

    [SerializeField] GameObject player;

    [SerializeField] NodeEventHolder[] nodesToReplace;
    [SerializeField] GameObject finalScore;

    bool hasRun = false;

    void Start(){
        conversation = conversationHolder.GetComponent<NPCConversation>();
        gameRunningManager = gameRunningManagerHolder.GetComponent<GameIsRunning>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasRun || gameRunningManager.GetTimeRunning() < gameLengthInSeconds) return;
        hasRun = true;
        // Start a coroutine
        StartCoroutine(EndGame());
        
    }

    IEnumerator EndGame()
    {
        yield return null;

        // If you reach this point, the game is over

        gameRunningManager.StopGame();
        
        // Start the conversation
        ConversationManager.Instance.StartConversation(conversation);
        int moneys = player.GetComponent<PlayerInventoryManager>().money;
        ConversationManager.Instance.SetInt("Money", moneys);
        finalScore.GetComponent<TextMeshProUGUI>().text = "$" + moneys;
    }

    public float GetTimeLeft()
    {
        return gameLengthInSeconds - gameRunningManager.GetTimeRunning();
    }

    public void SetGameLength(int seconds)
    {
        gameLengthInSeconds = seconds;
    }
}
