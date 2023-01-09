using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantState : MonoBehaviour
{

    public CropType currentCrop = CropType.NoCrop;
    public TileCropState currentPlantedState = TileCropState.Cleared;

    float secondsBeforeGrow = 0f;

    [SerializeField] Sprite clear;
    [SerializeField] Sprite ungrownCandyCane;
    [SerializeField] Sprite ungrownCoal;
    [SerializeField] Sprite ungrownTree;
    [SerializeField] Sprite grownCandyCane;
    [SerializeField] Sprite grownCoal;
    [SerializeField] Sprite grownTree;

    public GameObject gameRunningManager;

    GrowSpeedManager gsm;

    AudioSource growSound;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = clear;
        gsm = transform.parent.GetComponent<GrowSpeedManager>();
        growSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlantedState == TileCropState.SeedsPlanted && gameRunningManager.GetComponent<GameIsRunning>().gameIsRunning)
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
            GetComponentsInChildren<SpriteRenderer>()[1].sprite = grownCandyCane;
        }
        else if (currentCrop == CropType.Coal)
        {
            GetComponentsInChildren<SpriteRenderer>()[1].sprite = grownCoal;
        }
        else if (currentCrop == CropType.Tree)
        {
            GetComponentsInChildren<SpriteRenderer>()[1].sprite = grownTree;
        }

        growSound.Play();
    }
    
    public void PlantSeeds(int cropType)
    {
        currentCrop = (CropType)cropType;
        currentPlantedState = TileCropState.SeedsPlanted;

        print("I now have " + currentCrop.ToString() + " seeds planted!");

        secondsBeforeGrow = gsm.GetGrowthLength();

        if (currentCrop == CropType.CandyCane)
        {
            GetComponentsInChildren<SpriteRenderer>()[1].sprite = ungrownCandyCane;
        }
        else if (currentCrop == CropType.Coal)
        {
            GetComponentsInChildren<SpriteRenderer>()[1].sprite = ungrownCoal;
        }
        else if (currentCrop == CropType.Tree)
        {
            GetComponentsInChildren<SpriteRenderer>()[1].sprite = ungrownTree;
        }
    }

    public CropType Harvest()
    {
        CropType ret = currentCrop;
        
        currentPlantedState = TileCropState.Cleared;
        GetComponentsInChildren<SpriteRenderer>()[1].sprite = null;

        print("Harvested!");

        return ret;
    }
}
