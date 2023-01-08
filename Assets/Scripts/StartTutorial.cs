using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class StartTutorial : MonoBehaviour
{
    [SerializeField] NPCConversation tutorialConversation;
    [SerializeField] GameObject gameRunningManager;

    // Start is called before the first frame update
    void Start()
    {
        ConversationManager.Instance.StartConversation(tutorialConversation);
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
