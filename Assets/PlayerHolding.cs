using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlantState;

public class PlayerHolding : MonoBehaviour
{
    public enum CropType
    {
        None = 1,
        CandyCane = 2,
        Coal = 3,
        Tree = 4
    };

    public CropType holding = CropType.None;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        holding = CropType.Coal;
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            print("Hi");
            if (holding == CropType.None)
            {
                // Pick up
                }
            else
            {

                // Find the single nearest TRIGGER on the "Ground" layer in a circle
                // This is all 2d
                
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f, LayerMask.GetMask("Ground"));

                // order the colliders by distance
                System.Array.Sort(hitColliders, (x, y) => Vector2.Distance(transform.position, x.transform.position).CompareTo(Vector2.Distance(transform.position, y.transform.position)));
                
                int i = 0;
                while (i < hitColliders.Length)
                {

                    if (hitColliders[i].gameObject.tag == "Ground")
                    {
                        // We found a ground tile
                        // Check if it has a PlantState script
                        PlantState plantState = hitColliders[i].gameObject.GetComponent<PlantState>();
                        if (plantState != null)
                        {
                            // We found a plant state script
                            // Check if it's in the right state
                            if ((PlantState.TileCropState) plantState.currentPlantedState == PlantState.TileCropState.Cleared)
                            {
                                // We can plant seeds
                                plantState.PlantSeeds((int)holding);
                                holding = CropType.None;
                                return;
                            }
                        }
                    }
                    i++;
                }
            }
        }
    }
}
