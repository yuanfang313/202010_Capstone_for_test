using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleCanvas : MonoBehaviour
{
    public Transform centerEyeTransform;
    public GameObject canvas;
    public List<GameObject> tooltip = new List<GameObject>();

    private Vector3 centerEyePosition;
    private Vector3 canvasPositionAnchor;
    
    private bool buttonIsPressed = false;
    public bool canvasHadOpened = false;

    private void Awake()
    {
        ControllerStatus.ButtonDown += buttonHadPressed;    
    }

    private void OnDestroy()
    {
        ControllerStatus.ButtonDown -= buttonHadPressed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canvasHadOpened && buttonIsPressed)
        { 
            canvas.SetActive(true);
            canvas.transform.position = SetCanvasPosition();
            canvasHadOpened = true;
        }
        else if (canvasHadOpened && buttonIsPressed)
        {
            canvas.SetActive(false);
            canvasHadOpened = false;
        }

        if (!canvasHadOpened)
        {
            tooltip[0].SetActive(false);
            tooltip[1].SetActive(false);
            tooltip[2].SetActive(false);
            tooltip[3].SetActive(false);
            tooltip[4].SetActive(false);
            tooltip[5].SetActive(false);
            tooltip[6].SetActive(false);
            tooltip[7].SetActive(false);
            tooltip[8].SetActive(false);
        }
    }

    private void buttonHadPressed (bool buttonPressed)
    {
        buttonIsPressed = buttonPressed;
    }

    private Vector3 SetCanvasPosition()
    {
        centerEyePosition = centerEyeTransform.position;

        float y = centerEyePosition.y - 0.9f;
        if(y >= 0.5f)
        {
            canvasPositionAnchor = new Vector3(centerEyePosition.x, y, centerEyePosition.z + 2.0f);
        }
        else if (y < 0.5f)
        {
            canvasPositionAnchor = new Vector3(centerEyePosition.x, 0.5f, centerEyePosition.z + 2.0f);
        }
        
        return canvasPositionAnchor;
    }
}
