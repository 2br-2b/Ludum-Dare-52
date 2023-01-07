using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    [SerializeField] float speed = 0.1f;
    [SerializeField] float drag = 0.95f;

    // Update is called once per frame
    void Update()
    {

        Vector2 currentSpeed = GetComponent<Rigidbody2D>().velocity;
        currentSpeed.x += Input.GetAxis("Horizontal") * speed;
        currentSpeed.x *= drag;
        currentSpeed.y += Input.GetAxis("Vertical") * speed;
        currentSpeed.y *= drag;
        GetComponent<Rigidbody2D>().velocity = currentSpeed;

        // This will be handled by sprites/animations
        /*if(Vector3.Magnitude(currentSpeed) > 0.1)
        {
            transform.forward = currentSpeed;
        }*/


    }
}
