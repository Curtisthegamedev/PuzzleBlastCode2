using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Player : MonoBehaviour
{
    //sets bool variables needed to toggle flip and attack functions. 
    public bool isLookingRight;
    public static bool firePower, CanFireLazer = false, isInvisable = false, 
        hasArmour = false, hasSword = false;
    
    private bool onGround, GravityFlip = false; //sets variables for movement. 
    private bool hasLaserGun = true; //change this variable later after testing
    private float moveValue, speed = 10.0f, mass = 1.0f;
    private GameObject LaserSpawnPoint;
    private Rigidbody2D instanceOfBallShot, instanceOfLaserShot, rb;
    private Transform myTransform;
    private Vector3 velocity, accel;

    //shooting variables for attack function.
    [SerializeField] float shootTimer; 
    [SerializeField] float rateOfFire;
    [SerializeField] float forceOfShot;
    //---------------------------------
    [SerializeField] float JumpForce, lowerJump;
    [SerializeField] Animator anim;
    [SerializeField] GameObject ball, LaserBolt, sword,
        LaserGun, SwordWhileSwinging;
    [SerializeField] SpriteRenderer playerSpriteRenderer; //Get the sprite renderer
    [SerializeField] Transform shotSpawn;

    float horizontal;
    private void Awake()
    {
        //makes sure script has ridgidbody component and that mass is set. 
        rb = GetComponent<Rigidbody2D>();
        rb.GetComponent<Rigidbody2D>();
        rb.mass = 1.0f;
        myTransform = GetComponent<Transform>();
    } 

    //flips character based on where the player faces. 
    void Flip(float horizontal)
    {
        if (horizontal > 0 && !isLookingRight || horizontal < 0 && isLookingRight)
        {
            isLookingRight = !isLookingRight;
            transform.Rotate(0.0f, 180f, 0f);  
        }
    }

    //if player colldes with ground set animator bool and script bool to true. 
    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "SwordAttackPowerUp")
        {
            //I am still working on the Swords function.
            //GameObject swordWeapon = Instantiate(sword, shotSpawn.position, Quaternion.identity);
            hasSword = true;
            hasLaserGun = false; 
            sword.SetActive(true);
            LaserGun.SetActive(false); 
            Destroy(c.gameObject); 
        }

        if (c.gameObject.tag == "LaserGun")
        {
            Destroy(c.gameObject); 
            hasSword = false;
            hasLaserGun = true;
            LaserGun.SetActive(true);
            sword.SetActive(false);
            SwordWhileSwinging.SetActive(false); 
            //Instantiate(LaserGun, shotSpawn.position, shotSpawn.rotation); 
        }

        if (c.gameObject.tag == "Armour" && ArmourBar.armourAmount < 3)
        {
            ArmourBar.armourAmount = ArmourBar.armourAmount + 1;
            Destroy(c.gameObject); 
        }

        if (c.gameObject.tag == "Ground")
        {
            onGround = true;
            anim.SetBool("onGround", true); 
        }

        //loads gameOver scene when the player dies. 
        if(c.gameObject.tag == "Enemy" || c.gameObject.tag == "EProjectile")
        {
            //EnemyFlyingDrones are listed as a projectile meaning they and other projectiles will be 
            //destroyed when they touch the player
            if(c.gameObject.tag == "EProjectile")
            {
                Destroy(c.gameObject); 
            }
            //NOTE MUST ADD TEMP INVINCIBILITY
            if(!hasArmour)
            {
                Health.life--; 
            }
            else if (hasArmour)
            {
                ArmourBar.armourAmount--; 
            }
        }

        if (c.gameObject.tag == "CherryPowerUp")
        {
            speed = speed + 5;
            Destroy(c.gameObject);
        }

        if (c.gameObject.tag == "InvisiblePotion")
        {
            playerSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
            isInvisable = true;
            Destroy(c.gameObject);
            StartCoroutine(waitAndTurnOffInvisibility());
        }

        //sets the firepower attack bool true. 
        if (c.gameObject.tag == "StrawBerry")
        {
            firePower = true; 
        }
        
        if(c.gameObject.tag == "Pie")
        {
            Health.life++;
            Destroy(c.gameObject); 
        }
    }

    private IEnumerator waitAndTurnOffInvisibility()
    {
        yield return new WaitForSeconds(15);
        playerSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        isInvisable = false; 
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        //if the player collides with RGravity they will flip upside down allowing the to walk upside down. 
        //gravity will also be pushing the player up instead of down. 
       if(c.gameObject.tag == "RGravity")
        {
            myTransform.Rotate(0, 0, 540);
            rb.gravityScale = -1;
            GravityFlip = true;
        }
       //Ngravity will flip the player back to normal as well as set gravity back to normal. 
       if(c.gameObject.tag == "NGravity")
        {
            myTransform.Rotate(0, 0, -540);  
            rb.gravityScale = 1;
            GravityFlip = false; 
        }
       if(c.gameObject.tag == "Death")
        {
            SceneManager.LoadScene("GameOver"); 
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "RGravity")
        {
            Flip(-horizontal); 
        }
        if(col.gameObject.tag == "NGravity")
        {
            Flip(horizontal); 
        }
    }

    //on ground bools are always true when the player is onGround. 
    private void OnCollisionStay2D(Collision2D collision)
    {
        onGround = true;
        anim.SetBool("onGround", true); 
    }
    //on ground bools are flase if player leaves the ground.
    private void OnCollisionExit2D(Collision2D c)
    {
        //if(c.gameObject.tag == "Ground")
        //{
            onGround = false;
            anim.SetBool("onGround", false); 
        //}
    }

    private void SwordAttack()
    {
        if(hasSword && Input.GetKeyDown(KeyCode.E))
        {
            sword.SetActive(false);
            SwordWhileSwinging.SetActive(true); 
        }
        else if(hasSword && Input.GetKeyUp(KeyCode.E))
        {
            sword.SetActive(true);
            SwordWhileSwinging.SetActive(false); 
        }
    }


    private void Update()
    {
        SwordAttack();
        horizontal = Input.GetAxis("Horizontal");
        if(!GravityFlip)
        {
            Flip(horizontal);
        }
        else
        {
            Flip(-horizontal); 
        }
        moveValue = Input.GetAxisRaw("Horizontal");

        //Debug.Log(moveValue); 
        if (onGround)
        {
            anim.SetBool("onGround", true); 
            //controls movement. 
            if(Input.GetButtonDown("Jump") && !GravityFlip)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.up * JumpForce;
            }
            else if(Input.GetButtonDown("Jump") && GravityFlip)
            {
                GetComponent<Rigidbody2D>().velocity = -Vector2.up * JumpForce;
                Debug.Log("upsidedown jump");
            }
        }

        if (rb)
        {
            rb.velocity = new Vector2(moveValue * speed, rb.velocity.y);
            anim.SetFloat("Moveing", Mathf.Abs(moveValue)); 
        }
        else
        {
            Debug.Log("this script does not have the rb");
        }
    }
}