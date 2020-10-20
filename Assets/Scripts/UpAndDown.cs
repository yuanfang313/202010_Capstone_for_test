using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    private float maxTimer = 1.0f;
    private float tempTimer = 0;
    public int derection = 1;
    private bool hitted = false;

    //private HoveringEffects hoverScript;

    private void Start()
    { 
      //hoverScript = GetComponent<HoveringEffects>();      
    }
    void Update()
    {

        float speedScale = Mathf.Pow(Mathf.Abs(tempTimer - maxTimer / 2f), 0.5f) * 0.5f + 0.1f;

        // the moving speed decreases towards the end points, and the value of decrease increase in the process
        Vector3 positionChanged = Vector3.up * transform.localScale.x * Time.deltaTime * 0.15f;

        tempTimer += Time.deltaTime;
        if (tempTimer >= maxTimer)
        {
            tempTimer = 0;
            derection = -derection;
        }

        if (hitted == false)
            transform.position = transform.position + (positionChanged * speedScale) * derection;
              
    }

    private void OnTriggerEnter(Collider other)
    {
        hitted = true;
    }

    private void OnTriggerExit(Collider other)
    {
        hitted = false;
    }
}
