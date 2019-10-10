using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//THIS SCRIPT IS ATTACHED TO THE PLAYER
public class LazerGun : MonoBehaviour
{
    //[SerializeField] GameObject shotSpawn;
    //[SerializeField] GameObject Shot;

    //    private void Update()
    //   {
    //        if(Player.CanFireLazer)
    //        {
    //            Instantiate(Shot, shotSpawn.transform.position, shotSpawn.transform.rotation); 
    //        }
    //    }

    [SerializeField] Transform firepoint;
    [SerializeField] GameObject bulletPrefab; 

    private void Update()
    {
        if
            (Input.GetKeyDown(KeyCode.E))
        {
            shoot(); 
        }
    }

    private void shoot()
    {
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation); 
    }

}
