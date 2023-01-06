using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionOnStage = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePositionOnStage.z = 0;
        transform.position = mousePositionOnStage;
    }
}
