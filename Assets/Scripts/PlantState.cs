using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantState : MonoBehaviour
{

    public CropType currentCrop = CropType.NoCrop;
    public TileCropState currentPlantedState = TileCropState.Cleared;

    float secondsBeforeGrow = 0f;

    [SerializeField] Sprite clear;
    [SerializeField] Sprite seeds;
    [SerializeField] Sprite grownCandyCane;
    [SerializeField] Sprite grownCoal;
    [SerializeField] Sprite grownTree;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = clear;
    }

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

        if(currentCrop == CropType.CandyCane)
        {
            GetComponent<SpriteRenderer>().sprite = grownCandyCane;
        }
        else if (currentCrop == CropType.Coal)
        {
            GetComponent<SpriteRenderer>().sprite = grownCoal;
        }
        else if (currentCrop == CropType.Tree)
        {
            GetComponent<SpriteRenderer>().sprite = grownTree;
        }
    }
    
    public void PlantSeeds(int cropType)
    {
        currentCrop = (CropType)cropType;
        currentPlantedState = TileCropState.SeedsPlanted;

        print("I now have " + currentCrop.ToString() + " seeds planted!");

        GetComponent<SpriteRenderer>().sprite = seeds;

        secondsBeforeGrow = Random.Range(10f, 30f);
    }

    public CropType Harvest()
    {
        CropType ret = currentCrop;
        
        currentPlantedState = TileCropState.Cleared;
        GetComponent<SpriteRenderer>().sprite = clear;

        print("Harvested!");

        return ret;
    }
}
