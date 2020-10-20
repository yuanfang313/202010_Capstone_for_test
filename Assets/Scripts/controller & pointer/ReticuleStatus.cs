using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReticuleStatus : MonoBehaviour
{
    public SpriteRenderer CircleRenderer;
    public Sprite openSprite;
    public Sprite CloseSprite;
    

    private new Camera camera = null;

    private void Awake()
    {
        PointerStatus.OnPointerUpdate += UpdateSprite;
        camera = Camera.main;
    }

    void Update()
    {
        transform.LookAt(camera.gameObject.transform);
    }

    private void OnDestroy()
    {
        PointerStatus.OnPointerUpdate -= UpdateSprite;
    }

    private void UpdateSprite(Vector3 point, GameObject hitObject)
    {
        transform.position = point;
        if (hitObject)
            CircleRenderer.sprite = CloseSprite;
        else
            CircleRenderer.sprite = openSprite;
    }
}
