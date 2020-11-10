using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed, sprintSpeed;
    public float sprintMultiplyer;
    public float maxSpeed;
    public float acceleration;
    public float timeToMaxSpeed;

    public int direction, oldDirection;

    public bool walking, sprinting;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = speed * sprintMultiplyer;
        acceleration = timeToMaxSpeed / maxSpeed;

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //sets the oldDirection to the new direction
        oldDirection = direction;

        //Checks if player is sprinting or walking
        if(Input.GetKey(KeyCode.LeftShift)) 
        {
            sprinting = true;
            walking = false;
        } else { sprinting = false; walking = true; }

        //Checks if the player is holding the a or d key, will return direction 0 when not holding
        if (Input.GetKey(KeyCode.D)) { direction = 1; } else if(Input.GetKey(KeyCode.A)) { direction = -1; } else { direction = 0; }     

        //makes the player slowly go faster when sprinting
        if(sprinting)
        {
            sprintSpeed += acceleration;
        }
        else { sprintSpeed = speed; }
        sprintSpeed = Mathf.Clamp(sprintSpeed, -maxSpeed, maxSpeed);

        if (oldDirection != direction)
        {
            sprintSpeed = speed;
        }
    }

    private void FixedUpdate()
    {
        if(walking)
        {
            rb.velocity = new Vector2(speed * direction, rb.velocity.y);
        }

        if(sprinting)
        {
            rb.velocity = new Vector2(sprintSpeed * direction, rb.velocity.y);
        }
        
    }
}
