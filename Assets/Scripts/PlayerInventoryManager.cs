using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static PlantState;

public class PlayerInventoryManager : MonoBehaviour
{

    public CropType holding = CropType.NoCrop;
    public bool holdingSeeds = false;
    public int money = 0;
    [SerializeField] GameObject debugManager;
    [SerializeField] GameObject moneyUIPrefab;
    [SerializeField] Transform canvasTransform;

    int numberOfPlants = 0;

    Animator anim;
    bool debug;

    [SerializeField] AudioClip buyCandyCane, buyCoal, buyTree;
    [SerializeField] AudioClip depositBad, depositGood;
    [SerializeField] AudioClip harvestCandyCane, harvestCoal, harvestTree;
    [SerializeField] AudioClip noMoney, genericError, sellToElfonzo;

    [SerializeField] AudioClip buyCropMagic, buyHotChocolate, somethingGrows, plantSomething;
        

    AudioSource sour;

    [SerializeField] GameObject gameRunningHolder;
    GameIsRunning grm;

    GameObject nearest;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        debug = debugManager.GetComponent<DebugState>().isDebug;
        if (debug)
        {
            money = 1000;
        }
        else
        {
            money = 30;
        }

        sour = GetComponent<AudioSource>();
        if (genericError == null) genericError = depositBad;
        if (sellToElfonzo == null) sellToElfonzo = depositGood;
        
        grm = gameRunningHolder.GetComponent<GameIsRunning>();
    }

    public void collectMoney(int amount)
    {
        money += amount;
        // Get the location on the canvas of this sprite
        MakeTextPopup("+$" + amount);
    }

    public void payMoney(int amount)
    {
        money -= amount;
        // Get the location on the canvas of this sprite
        MakeTextPopup("-$" + amount);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && grm.gameIsRunning)
        {
            // Try to interact

            // Check if the player is holding something
            if (holding == CropType.NoCrop)
            {
                // The player is holding nothing, so try to pick up a plant
                
                nearest = GetNearestTileInRange();

                if (nearest != null)
                {
                    PlantState plantState = nearest.GetComponent<PlantState>();

                    if (plantState.currentPlantedState == TileCropState.HarvestReady)
                    {

                        anim.ResetTrigger("Chop"); //TODO: uncomment
                        anim.ResetTrigger("Pick");


                        holding = plantState.Harvest();
                        holdingSeeds = false;
                        print("I am now holding " + holding.ToString());
                        
                        switch (holding)
                        {
                            case CropType.CandyCane:
                                anim.SetTrigger("Chop");
                                playSound(harvestCandyCane);
                                break;

                            case CropType.Tree:
                                anim.SetTrigger("Chop");
                                playSound(harvestTree);
                                break;

                            case CropType.Coal:
                                anim.SetTrigger("Pick");
                                playSound(harvestCoal);
                                break;

                            default:
                                break;
                        }
                    }
                    else if(plantState.currentPlantedState == TileCropState.SeedsPlanted)
                    {
                        string[] listOfResponses =
                        {
                            "It's not ready yet!",
                            "It hasn't sprouted yet!",
                            "Give it time to grow!"
                        };

                        MakeTextPopup(listOfResponses[Random.Range(0, listOfResponses.Length)]);

                        playSound(genericError);
                    }else if(plantState.currentPlantedState == TileCropState.Cleared)
                    {
                        // The player is holding nohing and the land is clear
                        playSound(genericError);
                    }
                }
                else
                {
                    // The player is holding nothing and is not near any plants
                }

            }
            else if(holdingSeeds)
            {

                // Find the single nearest TRIGGER on the "Ground" layer in a circle
                // This is all 2d

                nearest = GetNearestTileInRange();

                if (nearest != null)
                {
                    // We found a ground tile
                    // Check if it has a PlantState script
                    PlantState plantState = nearest.gameObject.GetComponent<PlantState>();
                    if (plantState != null)
                    {
                        // We found a plant state script
                        // Check if it's in the right state
                        if (plantState.currentPlantedState == TileCropState.Cleared)
                        {
                            // We can plant seeds
                            plantState.PlantSeeds((int)holding);
                            holding = CropType.NoCrop;
                            holdingSeeds = false;

                            string[] listOfResponses =
                            {
                                "Planted!"
                            };

                            MakeTextPopup(listOfResponses[Random.Range(0, listOfResponses.Length)]);
                            numberOfPlants++;

                            playSound(plantSomething);
                            return;
                        }
                        else if(plantState.currentPlantedState == TileCropState.SeedsPlanted)
                        {

                            string[] listOfResponses =
                            {
                                "Something else is planted here!",
                                "This spot is occupied!"
                            };

                            MakeTextPopup(listOfResponses[Random.Range(0, listOfResponses.Length)]);

                            playSound(genericError);
                        }
                        else if (plantState.currentPlantedState == TileCropState.HarvestReady)
                        {
                            string[] listOfResponses =
                            {
                                "Put down what you're holding first!"
                            };

                            MakeTextPopup(listOfResponses[Random.Range(0, listOfResponses.Length)]);

                            playSound(genericError);
                        }
                    }

                }
                else
                {
                    // The player is holding something and is not near any plant tiles

                    nearest = GetNearestBuybackInRange();

                    if(nearest != null)
                    {
                        // They are selling something to the buyback

                        SellItemBuyback();

                    }
                }
            }
            else
            {
                // The player is holding some grown plant

                GameObject dropoff = GetNearestDropoffInRange();
                if (dropoff != null)
                {
                    // The player is holding some grown plant and is near a dropoff
                    DropoffPlaceScript dropoffScript = dropoff.GetComponent<DropoffPlaceScript>();
                    try
                    {
                        dropoffScript.Deposit(holding);
                        holding = CropType.NoCrop;
                        numberOfPlants--;
                        playSound(depositGood);
                    }
                    catch (System.Exception e)
                    {
                        if(e.Message == "You can't deposit that here!")
                        {

                            string[] listOfResponses =
                            {
                                "You can't deposit that!",
                                "You don't need that!",
                                "Try selling to Elfonzo!"
                            };

                            MakeTextPopup(listOfResponses[Random.Range(0, listOfResponses.Length)]);

                            playSound(depositBad);
                        }
                    }

                }
                else
                {
                    nearest = GetNearestBuybackInRange();

                    if (nearest != null)
                    {
                        // They are selling a grown tree to the buyback

                        SellItemBuyback();

                    }
                }
            }

            // Check for a store
            
            nearest = GetNearestStoreInRange();
            if (nearest != null)
            {
                // The player is near a store
                Store store = nearest.GetComponent<Store>();

                if (store.cropSelling != CropType.NoCrop && holding != CropType.NoCrop)
                {
                    string[] listOfResponses =
                    {
                        "You're already holding something!",
                        "Put down what you're holding first!"
                    };

                    MakeTextPopup(listOfResponses[Random.Range(0, listOfResponses.Length)]);
                    playSound(genericError);
                    return; // The player is trying to buy something while holding something
                }

                if (store.price > money)
                {
                    string[] listOfResponses =
                    {
                        "Not enough money!",
                        "You're short some money"
                    };

                    MakeTextPopup(listOfResponses[Random.Range(0, listOfResponses.Length)]);

                    playSound(noMoney);
                }
                else
                {
                    if(store.cropSelling == CropType.NoCrop && (money + (5 * numberOfPlants)) < 10 + store.price)
                    {
                        MakeTextPopup("Don't softlock yourself!");
                        playSound(noMoney);
                        return;
                    }

                    payMoney(store.price);

                    if (store.cropSelling != CropType.NoCrop)
                    {
                        holding = store.cropSelling;
                        holdingSeeds = true;

                        switch (store.cropSelling)
                        {
                            case CropType.CandyCane:
                                playSound(buyCandyCane);
                                break;

                            case CropType.Coal:
                                playSound(buyCoal);
                                break;

                            case CropType.Tree:
                                playSound(buyTree);
                                break;
                        }
                    }
                    else
                    {
                        if(store.selling == "hot chocolate")
                        {
                            playSound(buyHotChocolate);
                        }
                        else
                        {
                            playSound(buyCropMagic);
                        }
                    }
                    store.buyCrop();
                    
                }

            }
        }

        if(debug && Input.GetKeyDown(KeyCode.E))
        {
            collectMoney(10);
        }
    }

    private GameObject GetNearestTileInRange()
    {
        // Find the single nearest TRIGGER on the "Ground" layer in a circle
        // This is all 2d

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f, LayerMask.GetMask("Ground"));

        // Order the colliders by distance
        System.Array.Sort(hitColliders, (x, y) => Vector2.Distance(transform.position, x.transform.position).CompareTo(Vector2.Distance(transform.position, y.transform.position)));

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.gameObject.tag == "Ground")
            {
                return collider.gameObject;
            }
        }

        return null;

    }

    private GameObject GetNearestStoreInRange()
    {
        // Find the single nearest TRIGGER on the "Ground" layer in a circle
        // This is all 2d

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f, LayerMask.GetMask("Store"));

        // Order the colliders by distance
        System.Array.Sort(hitColliders, (x, y) => Vector2.Distance(transform.position, x.transform.position).CompareTo(Vector2.Distance(transform.position, y.transform.position)));

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.gameObject.tag == "Store")
            {
                return collider.gameObject;
            }
        }

        return null;

    }

    private GameObject GetNearestDropoffInRange()
    {
        // Find the single nearest TRIGGER on the "Ground" layer in a circle
        // This is all 2d

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f, LayerMask.GetMask("Dropoff"));

        // Order the colliders by distance
        System.Array.Sort(hitColliders, (x, y) => Vector2.Distance(transform.position, x.transform.position).CompareTo(Vector2.Distance(transform.position, y.transform.position)));

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.gameObject.tag == "Dropoff")
            {
                return collider.gameObject;
            }
        }

        return null;

    }

    public void MakeTextPopup(string text)
    {
        Vector3 e = Camera.main.WorldToScreenPoint(transform.position);
        GameObject go = Instantiate(moneyUIPrefab, e, Quaternion.identity, canvasTransform);
        go.GetComponent<TextMeshProUGUI>().text = text;
    }

    private GameObject GetNearestBuybackInRange()
    {
        // Find the single nearest TRIGGER on the "Ground" layer in a circle
        // This is all 2d

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f, LayerMask.GetMask("Buyback"));

        // Order the colliders by distance
        System.Array.Sort(hitColliders, (x, y) => Vector2.Distance(transform.position, x.transform.position).CompareTo(Vector2.Distance(transform.position, y.transform.position)));

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.gameObject.tag == "Buyback")
            {
                return collider.gameObject;
            }
        }

        return null;

    }

    private void SellItemBuyback()
    {
        numberOfPlants--;
        holding = CropType.NoCrop;
        if(money <= 10)
        {
            collectMoney(5);
        }
        else
        {
            collectMoney(Random.Range(1, 4));
        }

        playSound(sellToElfonzo);
    }


    private void playSound(AudioClip sound)
    {
        if (sound != null)
        {
            sour.clip = sound;
            sour.Play();
        }
    }
}
