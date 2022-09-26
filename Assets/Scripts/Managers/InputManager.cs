using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoSingleton<InputManager>, Controls.IPlayerActions
{
    public Vector2 MovementValue { get; private set; }
    public Vector2 MouseValue { get; private set; }

    public bool IsShooting { get; private set; }
    public bool IsPause { get; private set; }

    public event Action DodgeEvent;
    public event Action ActionEvent_1;

    private Controls controls;

    void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();
    }

    void OnDestroy()
    {
        controls?.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        MouseValue = context.ReadValue<Vector2>();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        DodgeEvent?.Invoke();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.IsPause) { return; }

        if (context.performed)
        {
            IsShooting = true;
        }
        else if(context.canceled)
        {
            IsShooting = false;
        }
    }

    public void OnAction_1(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.IsPause) { return; }

        if (!context.performed) { return; }
        ActionEvent_1?.Invoke();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsPause = true;
        }
        else if (context.canceled)
        {
            IsPause = false;
        }
    }
}
