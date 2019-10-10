using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpotLight : EnemyFlyDrone
{
    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && !Player.isInvisable)
        {
            playerIsSpoted = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        if(c.gameObject.tag == "Player")
        {
            playerIsSpoted = false; 
        }
    }
}
