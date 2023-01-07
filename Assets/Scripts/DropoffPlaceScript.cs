using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class DropoffPlaceScript : MonoBehaviour
{
    [SerializeField] GameObject textField;
    [SerializeField] GameObject player;

    public bool hasTree = false;
    public bool hasDecoration = false;
    public CropType currentlyLookingFor;

    // Start is called before the first frame update
    void Start()
    {
        createNewOrder();
    }

    void createNewOrder()
    {
        int random = Random.Range(2, 4);
        currentlyLookingFor = (CropType)random;
        hasTree = false;
        hasDecoration = false;

        textField.GetComponent<TextMeshProUGUI>().text =
            "Next order: " + currentlyLookingFor.ToString();
        //+ "\n";
        print(textField.GetComponent<TextMeshProUGUI>().text);
    }

    public void Deposit(CropType holding)
    {
        print("Deposited " + holding.ToString());

        if (holding == CropType.Tree && !hasTree)
        {
            hasTree = true;
        }
        else if (holding == currentlyLookingFor && !hasDecoration)
        {
            hasDecoration = true;
        }
        
        if(hasTree && hasDecoration)
        {
            // Give money
            player.GetComponent<PlayerHolding>().money += 12;
            createNewOrder();
        }
        
    }
}
