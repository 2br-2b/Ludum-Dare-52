using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elfered : MonoBehaviour
{
    public bool hasShownUp = false;

    [SerializeField] GameObject gameRunningManagerObject;
    GameIsRunning grm;

    [SerializeField] GameObject dialogueHolder;

    // Start is called before the first frame update
    void Start()
    {
        grm = gameRunningManagerObject.GetComponent<GameIsRunning>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasShownUp && grm.GetTimeRunning() > 10)
        {
            hasShownUp = true;
            StartCoroutine(ShowUp());
        }
    }

    IEnumerator ShowUp()
    {
        yield return null;
        grm.PauseGame(false);

        ConversationManager.Instance.StartConversation(dialogueHolder.GetComponent<NPCConversation>());
    }
}
