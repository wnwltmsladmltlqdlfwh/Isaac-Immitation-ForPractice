using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class InputManager : Singleton<InputManager>
{
    public PlayerInputSystem playerInputSystem { get; private set; }
    public PlayerInputSystem.CharactorActionsMapActions charInput { get; private set; }

    public Vector2 moveDir;

    private void Awake()
    {
        playerInputSystem = new PlayerInputSystem();

        charInput = playerInputSystem.CharactorActionsMap;

        charInput.MoveChar.started += OnMoveChar;
        charInput.MoveChar.performed += OnMoveChar;
        charInput.MoveChar.canceled += OnMoveChar;
    }

    private void OnEnable()
    {
        playerInputSystem.Enable();
    }

    private void OnDisable()
    {
        playerInputSystem.Disable();
    }

    public void OnMoveChar(InputAction.CallbackContext callbackContext)
    {
        moveDir = callbackContext.ReadValue<Vector2>();
    }
}
