using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrrigationStore : Store
{

    [SerializeField] GameObject managerHolder;
    GrowSpeedManager gsm;
    
    

    void Start()
    {
        price = 7;
        cropSelling = CropType.NoCrop;
        gsm = managerHolder.GetComponent<GrowSpeedManager>();
        
    }

    override public void buyCrop()
    {
        gsm.ReduceGrowthLength();
    }
}
