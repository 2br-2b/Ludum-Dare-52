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

    TextMeshProUGUI textGui;

    string orderText;
    string[] listOfOrderTexts;

    // Start is called before the first frame update
    void Start()
    {
        listOfTextures = new Sprite[4];
        listOfTextures[0] = null;
        listOfTextures[1] = placedCandyCane;
        listOfTextures[2] = placedCoal;
        listOfTextures[3] = placedTree;

        listOfOrderTexts = new string[3];
        listOfOrderTexts[0] = "Next order:";
        listOfOrderTexts[1] = "New order:";
        listOfOrderTexts[2] = "Order up:";

        textGui = textField.GetComponent<TextMeshProUGUI>();
        
        createNewOrder();
        //listOfOrderTexts[3] = ":";
    }

    void createNewOrder()
    {
        orderText = listOfOrderTexts[Random.Range(0, listOfOrderTexts.Length)];

        int random = Random.Range(2, 4);
        currentlyLookingFor = (CropType)random;
        hasTree = false;
        hasDecoration = false;

        textGui.text =
            orderText+"\n - " + currentlyLookingFor.ToString() + "\n - " + CropType.Tree.ToString();
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
            textGui.text =
            orderText+"\n - " + currentlyLookingFor.ToString() + "\n x " + CropType.Tree.ToString();
        }
        else if (holding == currentlyLookingFor && !hasDecoration)
        {
            hasDecoration = true;
            GetComponentsInChildren<SpriteRenderer>()[1].sprite = listOfTextures[(int)holding - 1];
            textGui.text = orderText+"\n x " + currentlyLookingFor.ToString() + "\n - " + CropType.Tree.ToString();
        }
        
        if(hasTree && hasDecoration)
        {
            // Give money
            player.GetComponent<PlayerInventoryManager>().collectMoney(Random.Range(10, 40));
            createNewOrder();

            GetComponentsInChildren<SpriteRenderer>()[1].sprite = listOfTextures[0];
        }
        
    }
}
