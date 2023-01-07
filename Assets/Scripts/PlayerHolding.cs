using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlantState;

public class PlayerHolding : MonoBehaviour
{

    public CropType holding = CropType.NoCrop;
    public bool holdingSeeds = true;


    // Start is called before the first frame update
    void Start()
    {
        holding = CropType.CandyCane;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            print("Hi");
            if (holding == CropType.NoCrop)
            {
                GameObject nearest = GetNearestTileInRange();

                if (nearest != null)
                {
                    PlantState plantState = nearest.GetComponent<PlantState>();

                    if (plantState.currentPlantedState == TileCropState.HarvestReady)
                    {
                        holding = plantState.Harvest();
                        print("I am now holding " + holding.ToString());
                    }
                }

            }
            else if(holdingSeeds)
            {

                // Find the single nearest TRIGGER on the "Ground" layer in a circle
                // This is all 2d

                GameObject nearest = GetNearestTileInRange();

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
                            return;
                        }
                    }

                }
            }
            else
            {
                // The player is holding some grown plant
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
}
