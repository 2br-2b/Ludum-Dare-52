using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantState : MonoBehaviour
{

    public enum CropType
    {
        None = 1,
        CandyCane = 2,
        Coal = 3,
        Tree = 4
    };

    public enum TileCropState
    {
        Snow = 1,
        Cleared = 2,
        SeedsPlanted = 3,
        HarvestReady = 4
    };

    public CropType currentCrop = CropType.None;
    public TileCropState currentPlantedState = TileCropState.Cleared;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlantSeeds(int cropType)
    {
        currentCrop = (CropType)cropType;
        currentPlantedState = TileCropState.SeedsPlanted;

        print("I now have " + currentCrop.ToString() + " seeds planted!");

        GetComponent<Renderer>().enabled = false;
    }
}
