using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantState : MonoBehaviour
{

    public enum CropType
    {
        None,
        CandyCane,
        Coal,
        Tree
    };

    public enum TileCropState
    {
        Snow,
        Cleared,
        SeedsPlanted,
        HarvestReady
    };

    public CropType currentCrop = CropType.None;
    public TileCropState currentState = TileCropState.Snow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
