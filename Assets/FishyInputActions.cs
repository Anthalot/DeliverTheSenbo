// GENERATED AUTOMATICALLY FROM 'Assets/FishyInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @FishyInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @FishyInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""FishyInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""cf1f409c-5b2e-4d27-b5f2-fbdd69bcf1a3"",
            ""actions"": [
                {
                    ""name"": ""CursorMovement"",
                    ""type"": ""Value"",
                    ""id"": ""ccf40780-45b2-447d-bdbb-c753c6563f8a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JoystickMovement1"",
                    ""type"": ""PassThrough"",
                    ""id"": ""80d6cead-1327-440f-81d3-69338dec0f5e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1b26af1d-053f-4863-8e49-b194ef825177"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cf6b2b9e-1952-4d5f-92da-72dded2ae274"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JoystickMovement1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_CursorMovement = m_Player.FindAction("CursorMovement", throwIfNotFound: true);
        m_Player_JoystickMovement1 = m_Player.FindAction("JoystickMovement1", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_CursorMovement;
    private readonly InputAction m_Player_JoystickMovement1;
    public struct PlayerActions
    {
        private @FishyInputActions m_Wrapper;
        public PlayerActions(@FishyInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @CursorMovement => m_Wrapper.m_Player_CursorMovement;
        public InputAction @JoystickMovement1 => m_Wrapper.m_Player_JoystickMovement1;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @CursorMovement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCursorMovement;
                @CursorMovement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCursorMovement;
                @CursorMovement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCursorMovement;
                @JoystickMovement1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJoystickMovement1;
                @JoystickMovement1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJoystickMovement1;
                @JoystickMovement1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJoystickMovement1;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CursorMovement.started += instance.OnCursorMovement;
                @CursorMovement.performed += instance.OnCursorMovement;
                @CursorMovement.canceled += instance.OnCursorMovement;
                @JoystickMovement1.started += instance.OnJoystickMovement1;
                @JoystickMovement1.performed += instance.OnJoystickMovement1;
                @JoystickMovement1.canceled += instance.OnJoystickMovement1;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnCursorMovement(InputAction.CallbackContext context);
        void OnJoystickMovement1(InputAction.CallbackContext context);
    }
}
