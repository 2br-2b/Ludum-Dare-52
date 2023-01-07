using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryTextUpdater1 : MonoBehaviour
{
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string s = "Holding " + player.GetComponent<PlayerHolding>().holding.ToString();
        if (player.GetComponent<PlayerHolding>().holdingSeeds)
        {
            s += " seeds";
        }
        GetComponent<TextMeshProUGUI>().text = s;
    }
}
