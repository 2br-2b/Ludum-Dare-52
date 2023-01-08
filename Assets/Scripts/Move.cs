using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    [SerializeField] float speed = 0.2f;
    [SerializeField] float drag = 0.9f;
    [SerializeField] GameObject gameRunningManager;
    [SerializeField] GameObject debugManager;

    bool wasRunning = false;
    Vector2 lastSpeed = new Vector2(0, 0);

    Rigidbody2D rbody;
    Animator anim;

    void Start(){

        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (debugManager.GetComponent<DebugState>().isDebug)
        {
            speed = 0.5f;
        }
            
    }

    // Update is called once per frame
    void Update()
    {

        if (gameRunningManager.GetComponent<GameIsRunning>().gameIsRunning) 
        {
            if (!wasRunning)
            {
                rbody.velocity = lastSpeed;
                wasRunning = true;
            }
            
            Vector2 currentSpeed = rbody.velocity;
            currentSpeed.x += Input.GetAxis("Horizontal") * speed;
            currentSpeed.x *= drag;
            currentSpeed.y += Input.GetAxis("Vertical") * speed;
            currentSpeed.y *= drag;
            rbody.velocity = currentSpeed;

            Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (movement_vector != Vector2.zero)
            {
                anim.SetBool("IsWalking", true);
                anim.SetFloat("x", movement_vector.x);
                anim.SetFloat("y", movement_vector.y);
            }
            if (movement_vector == Vector2.zero){
                anim.SetBool("IsWalking", false);
            }
            
            // The direction the sprite is pointing in degrees, with right being 0
            float direction = Mathf.Atan2(currentSpeed.y, currentSpeed.x) * Mathf.Rad2Deg;

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
                lastSpeed = rbody.velocity;
                rbody.velocity = Vector2.zero;
                wasRunning = false;

            }
        }


    }

    public void increaseSpeed(float amount)
    {
        speed += amount;
    }
}
