using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishyInputs : MonoBehaviour
{
    FishyInputActions fishyInputActions;
    public Vector2 pointerPosition;
    public Vector2 joystickInput;

    void OnEnable()
    {
        if(fishyInputActions == null)
        {
            fishyInputActions = new FishyInputActions();

            fishyInputActions.Player.CursorMovement.performed += i => pointerPosition = i.ReadValue<Vector2>();
            fishyInputActions.Player.JoystickMovement1.performed += i => joystickInput = i.ReadValue<Vector2>();
        }
        fishyInputActions.Enable();
    }

    void OnDiable()
    {
        fishyInputActions.Disable();
    }
}
