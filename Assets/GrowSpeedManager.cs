using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowSpeedManager : MonoBehaviour
{
    [SerializeField] int minGrowSpeed = 10;
    [SerializeField] int maxGrowSpeed = 30;
    [SerializeField] int irrigationBoost = 2;

    public int GetGrowthLength()
    {
        return Random.Range(minGrowSpeed, maxGrowSpeed);
    }

    public void ReduceGrowthLength()
    {

        if (maxGrowSpeed >= irrigationBoost)
        {
            maxGrowSpeed -= irrigationBoost;

            if (minGrowSpeed >= irrigationBoost)
            {
                minGrowSpeed -= irrigationBoost;
            }
        }
    }
}
