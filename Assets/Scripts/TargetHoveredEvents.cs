using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHoveredEvents : MonoBehaviour
{
    public AudioSource audioSource_target;
    public AudioClip audioClip_target;
    private Vector3 originalPosition;

    private float yModifier = 0.7f;
    private float target_x;
    private float target_y;
    private float target_z;

    private void Start()
    {
        originalPosition = transform.position;
        target_x = transform.position.x;
        target_y = yModifier;
        target_z = transform.position.z;

    }

    private void OnTriggerEnter(Collider collider)
    {
        audioSource_target.PlayOneShot(audioClip_target);
        transform.position = new Vector3(target_x, target_y, target_z);
    }

    private void OnTriggerExit(Collider collider)
    {
        transform.position = originalPosition;
    }


}
