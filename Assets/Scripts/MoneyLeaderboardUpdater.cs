using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyLeaderboardUpdater : MonoBehaviour
{
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = "$" + player.GetComponent<PlayerInventoryManager>().money;
    }
}
