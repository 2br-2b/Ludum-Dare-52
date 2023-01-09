using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryTextUpdater1 : MonoBehaviour
{
    [SerializeField] GameObject player;
    PlayerInventoryManager invMan;
    CropType holding;
    string s;

    // Start is called before the first frame update
    void Start()
    {
        invMan = player.GetComponent<PlayerInventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        holding = invMan.holding;

        if (holding == CropType.NoCrop)
        {
            s = "Holding nothing";
        }
        else
        {
            s = "Holding " + invMan.holding.ToString();
            if (invMan.holdingSeeds)
            {
                s += " seeds";
            }
        }
        GetComponent<TextMeshProUGUI>().text = s;
    }
}
