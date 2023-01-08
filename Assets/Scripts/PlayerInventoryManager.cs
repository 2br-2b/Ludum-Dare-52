using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlantState;

public class PlayerInventoryManager : MonoBehaviour
{

    public CropType holding = CropType.NoCrop;
    public bool holdingSeeds = false;
    public int money = 0;
    [SerializeField] GameObject debugManager;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (debugManager.GetComponent<DebugState>().isDebug)
        {
            money = 1000;
        }
    }

    public void collectMoney(int amount)
    {
        money += amount;
    }

    // Update is called once per frame
    void Update()
    {

        GameObject nearest;



        if (Input.GetKeyDown(KeyCode.Space))
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
                        /* TODO: uncomment*/
                        switch (holding)
                        {
                            case CropType.CandyCane:
                                anim.SetTrigger("Chop");
                                break;

                            case CropType.Tree:
                                anim.SetTrigger("Chop");
                                break;

                            case CropType.Coal:
                                anim.SetTrigger("Pick");
                                break;

                            default:
                                break;
                        }
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
                            return;
                        }
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
                    dropoffScript.Deposit(holding);
                    holding = CropType.NoCrop;
                }
            }

            // Check for a store
            
            nearest = GetNearestStoreInRange();
            if (nearest != null)
            {
                // The player is near a store
                Store store = nearest.GetComponent<Store>();

                if (store.cropSelling != CropType.NoCrop && holding != CropType.NoCrop) return; // The player is trying to buy something while holding something

                if (store.price > money)
                {
                    print("Not enough money");
                    // TODO: not enough money
                }
                else
                {
                    money -= store.price;

                    if (store.cropSelling != CropType.NoCrop)
                    {
                        holding = store.cropSelling;
                        holdingSeeds = true;
                    }
                    store.buyCrop();
                }

            }
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
}
