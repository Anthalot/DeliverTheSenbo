using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    PlayerInputActions playerInputActions;
    Vector2 movementInput;
    public float horizontalInput;
    public float jump;
    public float dash;

    // When the object this script is attached to checks if the input actions is initialized.
    private void OnEnable()
    {
        if(playerInputActions == null)
        {
            playerInputActions = new PlayerInputActions();

            playerInputActions.Player.Move.performed += i => movementInput = i.ReadValue<Vector2>();
            playerInputActions.Player.Jump.performed += i => jump = i.ReadValue<float>();
            playerInputActions.Player.Dash.performed += i => dash = i.ReadValue<float>();
        }
        playerInputActions.Enable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    private void HandleMovementInput()
    {
        horizontalInput = movementInput.x;
    }
}
