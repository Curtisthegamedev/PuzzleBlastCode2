using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBullet : MonoBehaviour
{
    private float speed = 15.0f;
    private Transform PlayerPos;
    private Transform pos;

    private void Start()
    {
        PlayerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, speed * Time.deltaTime); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject); 
    }
}
