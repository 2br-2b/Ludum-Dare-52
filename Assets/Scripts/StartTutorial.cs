using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class StartTutorial : MonoBehaviour
{
    [SerializeField] NPCConversation tutorialConversation;
    [SerializeField] NPCConversation blankConversation;
    [SerializeField] GameObject gameRunningManager;
    [SerializeField] GameObject debugger;

    [SerializeField] GameObject musicManagerGameObject;
    MusicManager musicManager;

    [SerializeField] GameObject elfonzoShop;
    

    // Start is called before the first frame update
    void Start()
    {
        if (debugger.GetComponent<DebugState>().isDebug)
        {
            ConversationManager.Instance.StartConversation(blankConversation);
        }
        else
        {
            ConversationManager.Instance.StartConversation(tutorialConversation);
        }

        musicManager = musicManagerGameObject.GetComponent<MusicManager>();
    }

    // Subscribe to the OnConversationEnded event
    private void OnEnable()
    {
        ConversationManager.OnConversationEnded += OnConversationEnded;
    }

    // Unsubscribe from the OnConversationEnded event
    private void OnDisable()
    {
        ConversationManager.OnConversationEnded -= OnConversationEnded;
    }


    void OnConversationEnded()
    {
        //Debug.Log("A conversation has ended.");
        gameRunningManager.GetComponent<GameIsRunning>().StartGame();
    }
}
