using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTowardsMouse : MonoBehaviour
{
    float previousAngle = 0;
    public float speed = 0;
    [SerializeField] Quaternion qqqq;

    // Update is called once per frame
    void Update()
    {
        // Point at the mouse
        // Also print how many degrees the object just rotated
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        speed = Mathf.Abs(Mathf.Round((previousAngle - angle) * 100) / 100);

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        previousAngle = angle;
        
        /*if (speed > 3)
        {

            Collider[] colliders = Physics.OverlapBox(GetComponentInChildren<BoxCollider2D>().bounds.center, GetComponentInChildren<BoxCollider2D>().bounds.extents, qqqq, LayerMask.GetMask("Tool", "Ground"), QueryTriggerInteraction.Collide);

            foreach (Collider collider in colliders)
            {
                // Do something with the colliding object.
                print("hi!");
            }
        
        }*/

    }
}
