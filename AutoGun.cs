﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGun : MonoBehaviour
{
    [SerializeField] GameObject bolt;
    [SerializeField] Transform shootPoint;
    private bool justShot = false; 

    private void Update()
    {
        if(!justShot)
        {
            Instantiate(bolt, shootPoint.position, shootPoint.rotation);
            StartCoroutine(WaitAndSetJustShotBoolFalse()); 
            justShot = true; 
        }


    }

    private IEnumerator WaitAndSetJustShotBoolFalse()
    {
        yield return new WaitForSeconds(1);
        justShot = false; 
    }
}
