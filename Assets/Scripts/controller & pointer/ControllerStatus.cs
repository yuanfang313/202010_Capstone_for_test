using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerStatus : MonoBehaviour
{
    #region Events
    public static UnityAction<OVRInput.Controller, GameObject> OnControllerSource = null;
    public static UnityAction<bool, bool> TriggerDown = null;
    public static UnityAction<bool> ButtonDown = null;
    #endregion

    #region Anchors
    public GameObject LeftAnchor;
    public GameObject RightAnchor;
    public GameObject HeadAnchor;
    #endregion

    #region Input
    private Dictionary<OVRInput.Controller, GameObject> ControllerSets = null;
    private OVRInput.Controller InputSource = OVRInput.Controller.None;//detect where our input come from last
    private OVRInput.Controller Controller = OVRInput.Controller.None;//detect whatever the active controller state is
    private bool InputActive = true;//detect whether the headset is currently attached to somebody's face
    #endregion

    private bool changeController = true;
    private bool rightTriggerDown = false;
    private bool leftTriggerDown = false;
    private bool buttonDown = false;
    private bool rightControllerIsConnected = false;
    private bool leftControllerIsConnected = false;

    private void Awake()
    {
        OVRManager.HMDMounted += PlayerFound;
        OVRManager.HMDUnmounted += PlayerLost;

        ControllerSets = CreateControllerSets();
    }

    private void OnDestroy()
    {
        OVRManager.HMDMounted -= PlayerFound;
        OVRManager.HMDUnmounted -= PlayerLost;
    }

    private void Update()
    {
        //check for active input(if the user has the headset on)
        if (!InputActive)
            return;

        //check if a controller exists
        CheckForController();
        CheckControllerInput();
        CheckControllerTrigger();
        CheckControllerButton();
    }

    private void CheckForController()
    {
        OVRInput.Controller controllerCheck = Controller;

        //right controller
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTouch))
        {
            rightControllerIsConnected = true;
            controllerCheck = OVRInput.Controller.RTouch;
        } else
        {
            rightControllerIsConnected = false;
        }
            

        //left controller
        if (OVRInput.IsControllerConnected(OVRInput.Controller.LTouch))
        {
            leftControllerIsConnected = true;
            controllerCheck = OVRInput.Controller.LTouch;
        } else
        {
            leftControllerIsConnected = false;
        }
            
        //left & right controller
        if (OVRInput.IsControllerConnected(OVRInput.Controller.LTouch) ||
           OVRInput.IsControllerConnected(OVRInput.Controller.RTouch))
        {
            if (!changeController)
            {
                controllerCheck = OVRInput.Controller.LTouch;
            }
            else if (changeController)
            {
                controllerCheck = OVRInput.Controller.RTouch;
            }
        }

        // Update
        Controller = UpdateSource(controllerCheck, Controller);
    }

    private void CheckControllerInput()
    {
        if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.LTouch))
        {
            changeController = false;
        }
        else if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.RTouch))
        {
            changeController = true;
        }
    }

    private void CheckControllerTrigger()
    {
      if (leftControllerIsConnected || rightControllerIsConnected)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
                leftTriggerDown = true;
            else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
                leftTriggerDown = false;

            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                rightTriggerDown = true;
            else if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
                rightTriggerDown = false;
        }

        if (TriggerDown != null)
            TriggerDown(rightTriggerDown, leftTriggerDown);
    }
    
    private void CheckControllerButton()
    {
        if (leftControllerIsConnected || rightControllerIsConnected)
        {
            if (OVRInput.GetDown(OVRInput.Button.Two) || OVRInput.GetDown(OVRInput.Button.Four))
            {
                buttonDown = true;
            } else
            {
                buttonDown = false;
            }       
        }

        if (ButtonDown != null)
            ButtonDown(buttonDown);
    }
    

    private OVRInput.Controller UpdateSource(OVRInput.Controller check, OVRInput.Controller previous)
    {
        //if values are the same, return
        if (check == previous)
            return previous;

        //if (check != previous)
        //get controller object
        GameObject controllerObject = null;
        ControllerSets.TryGetValue(check, out controllerObject);

        //sent out "check" & "controllerObject"
        if (OnControllerSource != null)
            OnControllerSource(check, controllerObject);

        return check;
    }

    private void PlayerFound()
    {
        InputActive = true;
    }

    private void PlayerLost()
    {
        InputActive = false;
    }

    private Dictionary<OVRInput.Controller, GameObject> CreateControllerSets()
    {
        Dictionary<OVRInput.Controller, GameObject> newSets = new Dictionary<OVRInput.Controller, GameObject>()
        {
            {OVRInput.Controller.LTouch, LeftAnchor},
            {OVRInput.Controller.RTouch, RightAnchor}
        };
        return newSets;
    }
}
