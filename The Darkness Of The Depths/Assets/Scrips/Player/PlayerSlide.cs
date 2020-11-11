using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D bc;
    public Transform body;

    public float slideSpeed;
    public bool isSliding;

    public int rotation;

    [SerializeField] LayerMask groundMask;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && IsGrounded() && isSliding)
        {
            body.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
            isSliding = true;
        }
    }


    public bool IsGrounded()
    {
        RaycastHit2D raycasthit2d = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, groundMask);
        return raycasthit2d.collider != null;
    }
}
