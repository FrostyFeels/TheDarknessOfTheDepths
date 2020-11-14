using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public float speed;
    public PlayerGrapple grapple;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.down * speed * Time.fixedDeltaTime);
        Debug.Log(speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall") || collision.CompareTag("Floor"))
        {
            grapple.Hit(collision.gameObject, transform.position);
            Destroy(gameObject);
        }
        
    }
}
