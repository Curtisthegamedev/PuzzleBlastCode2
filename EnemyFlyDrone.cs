using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//INHERITS FROM ENEMY 
public class EnemyFlyDrone : Enemy
{
    [SerializeField] GameObject spotLight; 
    private Transform targetPlayer;
    [SerializeField] Transform SpawnBolt; 
    public GameObject bullet; 
    private bool isMoveing;
    public static bool playerIsSpoted = false;
    void Start()
    {
        //at the beggining I set the timer to 0 and the targetPlayer Transform variable to my Player game object(The heart). 
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void ShootPlayer()
    {
        if(playerIsSpoted && Time.time > lastBulletSpawnTime + rateOfThrowing)
        {
            Instantiate(bullet, SpawnBolt.position, SpawnBolt.rotation);
            lastBulletSpawnTime = Time.time; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall")
        {
            Flip(); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TriggerDirSwitch")
        {
            Flip();
        }
    }

    private IEnumerator WaitAndToggleSpotlight()
    {
        if(spotLight.activeInHierarchy)
        {
            yield return new WaitForSeconds(3.5f); 
            spotLight.SetActive(false);
        }
        else if(!spotLight.activeInHierarchy)
        {
            yield return new WaitForSeconds(3.5f); 
            spotLight.SetActive(true); 
        }
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
        ShootPlayer();
        StartCoroutine(WaitAndToggleSpotlight()); 
    }
}
