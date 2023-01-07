using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantState : MonoBehaviour
{

    public CropType currentCrop = CropType.NoCrop;
    public TileCropState currentPlantedState = TileCropState.Cleared;

    float secondsBeforeGrow = 0f;


    // Update is called once per frame
    void Update()
    {
        if (currentPlantedState == TileCropState.SeedsPlanted)
        {
            secondsBeforeGrow -= Time.deltaTime;

            if (secondsBeforeGrow <= 0)
            {
                growPlant();
            }
        }
    }

    public void growPlant()
    {
        print("fully grown!");
        currentPlantedState = TileCropState.HarvestReady;
        secondsBeforeGrow = 0;

        GetComponent<Renderer>().enabled = true;
    }
    
    public void PlantSeeds(int cropType)
    {
        currentCrop = (CropType)cropType;
        currentPlantedState = TileCropState.SeedsPlanted;

        print("I now have " + currentCrop.ToString() + " seeds planted!");

        GetComponent<Renderer>().enabled = false;
        
        secondsBeforeGrow = Random.Range(10f, 30f);
    }

    public CropType Harvest()
    {
        CropType ret = currentCrop;
        
        currentPlantedState = TileCropState.Cleared;
        GetComponent<Renderer>().enabled = false;

        print("Harvested!");
        
        return ret;
    }
}
