using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonHoveringEffects : MonoBehaviour
{
    public AudioSource buttonAudioSouce;
    private Vector3 originalPosition;
    private Vector3 changedPosition;
    private bool hadHit = false;
    private bool canvasHadOpened = false;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        originalPosition = transform.position;
        changedPosition = new Vector3(originalPosition.x, originalPosition.y - 0.03f, originalPosition.z + 0.03f);
        if (!hadHit)
        {
            buttonAudioSouce.Play();
            transform.position = changedPosition;
            hadHit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        transform.position = originalPosition;
        hadHit = false;
    }

}
