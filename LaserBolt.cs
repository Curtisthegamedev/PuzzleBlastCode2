using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBolt : MonoBehaviour
{
    private float speed = 20f;
    private Rigidbody2D rb; 
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = -transform.right * speed; 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Trigger" && collision.gameObject.tag != "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
