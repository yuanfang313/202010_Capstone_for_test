using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringEffects2 : MonoBehaviour
{
    public AudioSource audioSource_hover2;
    private bool hadHit = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!hadHit && other.tag == "reticuleCollider")
        {
            audioSource_hover2.Play();
            hadHit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        hadHit = false;
    }
}
