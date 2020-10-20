using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BtnStatus : MonoBehaviour
{
    public Sprite Btn;
    public Sprite hoveredBtn;
    public GameObject tooltip;

    private SpriteRenderer BtnRenderer;
    private AudioSource audioSource_HoverBtn;
    private Vector3 btnPosition;
    private bool btnHadHovered = false;
    private float x, y, z;

    private void Start()
    {
        audioSource_HoverBtn = GetComponent<AudioSource>();
        BtnRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!btnHadHovered)
        {
            btnHadHovered = true;
            audioSource_HoverBtn.Play();
            btnPosition = transform.position;
            x = transform.position.x;
            y = transform.position.y + 0.35f;
            z = transform.position.z + 0.15f;
        }

        BtnRenderer.sprite = hoveredBtn;
        if (tooltip != null)
        {
            tooltip.SetActive(true);
            tooltip.transform.position = new Vector3(x, y, z);
        }
    }

    private void OnTriggerExit(Collider ther)
    {
        BtnRenderer.sprite = Btn;
        if(tooltip != null)
        {
            tooltip.SetActive(false);
        }
        btnHadHovered = false;
    }
}
