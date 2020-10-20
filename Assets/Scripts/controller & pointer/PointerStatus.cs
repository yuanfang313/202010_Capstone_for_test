using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PointerStatus : MonoBehaviour
{
    public float Distance = 10.0f;
    public LineRenderer LineRenderer = null;
    public Transform startPointAnchor_R;
    public Transform startPointAnchor_L;
    public LayerMask EverythingMask = 0;
    public LayerMask InteractableMask = 0;

    public static UnityAction<Vector3, GameObject> OnPointerUpdate = null;
    public static UnityAction<GameObject> OnPointerUpdateForObject = null;


    private Transform CurrentOrigin = null;
    private Transform CurrentStartPoint = null;
    private OVRInput.Controller CurrentController = OVRInput.Controller.None;
    private Vector3 endPosition;
    private GameObject currentObject = null;
    private bool hitObject = false;

    private void Awake()
    {
        ControllerStatus.OnControllerSource += UpdateOrigin;
    }

    private void OnDestroy()
    {
        ControllerStatus.OnControllerSource -= UpdateOrigin;
    }

    private void Start()
    {
        SetLineColor();
    }

    private void Update()
    {
        Vector3 hitPoint = UpdateLine();//current position of endPosition
        currentObject = UpdatePointerStatus();//current gameObject being hitted

        SetLineColor();
        //sent out OnPointerUpdate
        if (OnPointerUpdate != null)
            OnPointerUpdate(hitPoint, currentObject);

        //sent out OnPointerUpdateForObject
        if (OnPointerUpdateForObject != null)
            OnPointerUpdateForObject(currentObject);
    }

    private Vector3 UpdateLine()
    {
        //Create ray
        RaycastHit hit = CreateRaycast(EverythingMask);

        //if didn't hit (default end)
        endPosition = CurrentOrigin.position + (CurrentOrigin.forward * Distance);

        //if hit
        if (hit.collider != null)
            endPosition = hit.point;

        //set position

        if (CurrentController == OVRInput.Controller.RTouch)
        {
            CurrentStartPoint = startPointAnchor_R;
        }
        else if (CurrentController == OVRInput.Controller.LTouch)
        {
            CurrentStartPoint = startPointAnchor_L;
        }

        LineRenderer.SetPosition(0, CurrentStartPoint.position);
        LineRenderer.SetPosition(1, endPosition);

        return endPosition;
    }

    //return gameObject in the "Interactable" layer which are navigating elements
    public GameObject UpdatePointerStatus()
    {
        //create ray
        RaycastHit hit = CreateRaycast(InteractableMask);

        //check hit
        if (hit.collider)
        {
            hitObject = true;
            return hit.collider.gameObject;
        }
        else
        {
            //return
            hitObject = false;
            return null;
        }
    }

    private void UpdateOrigin(OVRInput.Controller controller, GameObject controllerObject)
    {
        //Set origin of pointer
        CurrentOrigin = controllerObject.transform;
        CurrentController = controller;
        //Is the laser visible?
    }

    private RaycastHit CreateRaycast(int layer)
    {
        RaycastHit hit;
        Ray ray = new Ray(CurrentOrigin.position, CurrentOrigin.forward);
        Physics.Raycast(ray, out hit, Distance, layer);
        return hit;
    }

    private void SetLineColor()
    {
        if (!LineRenderer)
            return;
        // !hitObject
        Color startColor1 = Color.white;
        Color endColor1 = Color.white;
        endColor1.a = 0.0f;
        // hitObject
        Color startColor2 = new Color(0.4f, 1.0f, 1.0f, 1.0f);
        Color endColor2 = new Color(0.4f, 1.0f, 1.0f, 0.0f);

        if (hitObject)
        {
            LineRenderer.startColor = startColor2;
            LineRenderer.endColor = endColor2;
        }
        else if (!hitObject)
        {
            LineRenderer.startColor = startColor1;
            LineRenderer.endColor = endColor1;
        }
    }
}
