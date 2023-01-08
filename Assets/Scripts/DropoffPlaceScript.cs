using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class DropoffPlaceScript : MonoBehaviour
{
    [SerializeField] GameObject textField;
    [SerializeField] GameObject player;

    [SerializeField] Sprite placedTree;
    [SerializeField] Sprite placedCandyCane;
    [SerializeField] Sprite placedCoal;

    public bool hasTree = false;
    public bool hasDecoration = false;
    public CropType currentlyLookingFor;

    public Sprite[] listOfTextures;

    // Start is called before the first frame update
    void Start()
    {
        createNewOrder();

        listOfTextures = new Sprite[4];
        listOfTextures[0] = null;
        listOfTextures[1] = placedCandyCane;
        listOfTextures[2] = placedCoal;
        listOfTextures[3] = placedTree;
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
            GetComponentsInChildren<SpriteRenderer>()[1].sprite = listOfTextures[(int)CropType.Tree - 1];
        }
        else if (holding == currentlyLookingFor && !hasDecoration)
        {
            hasDecoration = true;
            GetComponentsInChildren<SpriteRenderer>()[1].sprite = listOfTextures[(int)holding - 1];
        }
        
        if(hasTree && hasDecoration)
        {
            // Give money
            player.GetComponent<PlayerInventoryManager>().money += 12;
            createNewOrder();

            GetComponentsInChildren<SpriteRenderer>()[1].sprite = listOfTextures[0];
        }
        
    }
}
