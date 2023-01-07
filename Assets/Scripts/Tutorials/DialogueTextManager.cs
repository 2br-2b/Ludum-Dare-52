using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTextManager : MonoBehaviour
{
    string[] dialogueText = {
        "Hi!", "It seems like you're new here. If this isn't your first time, feel free to press the button below!",
        "I see you're trying to make a bit of money. Well, it's Christmas Eve, and we need to finish getting Christmas trees for all of the children!",
        "You know the drill; good kids get candy canes on their trees, and bad kids get coal. Run and buy the resources you need from the stores below and bring me them.",
        "Well, enough sitting around. It's Christmas Eve, and a storm's coming. You better hurry!"
    };

    int dialogueIndex = 0;

    // Update is called once per frame
    void Update()
    {
        
    }
}
