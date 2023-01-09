using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSnowOverTime : MonoBehaviour
{
    SnowController controller;
    float secondsSinceStart = 0f;
    float defaultSnowIntensity, defaultWindIntensity, defaultFogIntensity;
    float lastTick = 0f;

    [SerializeField] float minutesToMaxSnow, minutesToMaxWind, minutesToMaxFog;

    [SerializeField] GameObject gameRunningManager;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<SnowController>();
        defaultSnowIntensity = controller.snowIntensity;
        defaultWindIntensity = controller.windIntensity;
        defaultFogIntensity = controller.fogIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameRunningManager.GetComponent<GameIsRunning>().gameIsRunning)
        {
            secondsSinceStart += Time.deltaTime;

            if (secondsSinceStart - lastTick > 60)
            {
                float q = defaultSnowIntensity + (secondsSinceStart / (60f * minutesToMaxSnow)) * (1 - defaultSnowIntensity);
                controller.snowIntensity = q > 1 ? 1 : q;

                q = defaultWindIntensity + (secondsSinceStart / (60f * minutesToMaxWind)) * (1 - defaultWindIntensity);
                //controller.windIntensity = q > 1 ? 1 : q;

                q = defaultFogIntensity + (secondsSinceStart / (60f * minutesToMaxFog)) * (1 - defaultFogIntensity);
                controller.fogIntensity = q > 1 ? 1 : q;

                lastTick = secondsSinceStart;
            }
        }

    }
}
