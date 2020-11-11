using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    public float jumpCount;
    public float jumpForce;

    public bool jump;

    public float fJumpPressedRemember;
    public float fJumpPressedRememberTime = 0.2f;

    public float smallGravity, heavyGrafity;

    BoxCollider2D bc;
    Rigidbody2D rb;
    [SerializeField] LayerMask groundMask;
    
    void Start()
    {
        bc = gameObject.GetComponentInChildren<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

  
    void Update()
    {
        //Makes the player jump even when they press the button a little to early
        fJumpPressedRemember -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fJumpPressedRemember = fJumpPressedRememberTime;
        }
        if (isGrounded() && fJumpPressedRemember > 0f)
        {
            jump = true;
            jumpCount = 1;
            fJumpPressedRemember = 0f;
        }


        //Changes gravity depeneding on if you hold the button or not
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = heavyGrafity;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = smallGravity;
        }
        else
        {
            rb.gravityScale = 5f;
        }
    }

    private void FixedUpdate()
    {
        if(jump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jump = false;
        }    
    }



    public bool isGrounded()
    {
        RaycastHit2D raycasthit2d = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, groundMask);
        return raycasthit2d.collider != null;
    }
}
