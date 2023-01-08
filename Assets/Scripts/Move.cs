using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    [SerializeField] float speed = 0.2f;
    [SerializeField] float drag = 0.9f;
    [SerializeField] GameObject gameRunningManager;

    bool wasRunning = false;
    Vector2 lastSpeed = new Vector2(0, 0);

    // Update is called once per frame
    void Update()
    {

        if (gameRunningManager.GetComponent<GameIsRunning>().gameIsRunning) 
        {
            if (!wasRunning)
            {
                GetComponent<Rigidbody2D>().velocity = lastSpeed;
                wasRunning = true;
            }
            
            Vector2 currentSpeed = GetComponent<Rigidbody2D>().velocity;
            currentSpeed.x += Input.GetAxis("Horizontal") * speed;
            currentSpeed.x *= drag;
            currentSpeed.y += Input.GetAxis("Vertical") * speed;
            currentSpeed.y *= drag;
            GetComponent<Rigidbody2D>().velocity = currentSpeed;

            GetComponent<Animator>().SetFloat("Speed", currentSpeed.magnitude); //TODO: uncomment

            // The direction the sprite is pointing in degrees, with right being 0
            float direction = Mathf.Atan2(currentSpeed.y, currentSpeed.x) * Mathf.Rad2Deg;
            GetComponent<Animator>().SetFloat("Direction", direction); //TODO: uncomment

            // This will be handled by sprites/animations
            /*if(Vector3.Magnitude(currentSpeed) > 0.1)
            {
                transform.forward = currentSpeed;
            }*/
        }
        else
        {
            if (wasRunning)
            {
                lastSpeed = GetComponent<Rigidbody2D>().velocity;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                wasRunning = false;
            }
        }


    }

    public void increaseSpeed(float amount)
    {
        speed += amount;
    }
}
