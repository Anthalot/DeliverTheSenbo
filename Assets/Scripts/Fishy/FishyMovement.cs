using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishyMovement : MonoBehaviour
{
    public FishyInputs fishyInputs;
    public float movementSpeed;

    bool gamepad;
    bool keyboard;

    void Update()
    {
        if(Gamepad.current.IsActuated() && !Keyboard.current.IsActuated())
        {
            gamepad = true;
            keyboard = false;
        }
        else if(Keyboard.current.IsActuated() && !Gamepad.current.IsActuated())
        {
            gamepad = false;
            keyboard = true;
        }

        if(gamepad) transform.position += new Vector3(fishyInputs.joystickInput.x * movementSpeed * Time.unscaledDeltaTime, fishyInputs.joystickInput.y * movementSpeed * Time.unscaledDeltaTime, 0);
        if(keyboard)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(fishyInputs.pointerPosition);
            transform.position = mousePos;
        }
    }
}
