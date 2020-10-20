using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringEffects : MonoBehaviour
{ 
    public GameObject portalParticle;
    public Sprite sprite_Original;
    public Sprite sprite_Hovered;
    public bool hadHit = false;

    private SpriteRenderer BtnSpriteRenderer;
    private Transform originalTransform;
    private AudioSource audioSource_hover;
    private Vector3 originalScale;
   
    private void Start()
    { 
        originalTransform = transform;
        originalScale = transform.localScale;
        BtnSpriteRenderer = GetComponent<SpriteRenderer>();
        audioSource_hover = GetComponent<AudioSource>();
        if (portalParticle != null)
            portalParticle.SetActive(false);
    }
 
    private void OnTriggerEnter(Collider other)
    {
        BtnSpriteRenderer.sprite = sprite_Hovered;

        if (!hadHit)
        {
            transform.localScale = transform.localScale * 1.1f;
            hadHit = true;
            audioSource_hover.Play();
            if (portalParticle != null)
                portalParticle.SetActive(true);

        }  
    }

    private void OnTriggerExit(Collider other)
    {
        BtnSpriteRenderer.sprite = sprite_Original;
        transform.localScale = originalScale;
        hadHit = false;

        if (portalParticle != null)
            portalParticle.SetActive(false);
    }

    
}
