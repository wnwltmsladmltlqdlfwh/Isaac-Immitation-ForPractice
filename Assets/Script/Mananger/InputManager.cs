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
    public Vector2 bulletDir;

    private bool isShooting = false;
    public bool IsShooting
    {
        get { return isShooting; }
        private set { isShooting = value; }
    }

    private void Awake()
    {
        playerInputSystem = new PlayerInputSystem();

        charInput = playerInputSystem.CharactorActionsMap;

        charInput.MoveChar.started += OnMoveChar;
        charInput.MoveChar.performed += OnMoveChar;
        charInput.MoveChar.canceled += OnMoveChar;

        charInput.BulletDir.started += OnBulletDir;
        charInput.BulletDir.performed += OnBulletDir;
        charInput.BulletDir.canceled += OnBulletDir;

        charInput.FireBomb.started += OnFireBomb;
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
        if (PlayerManager.Instance.playerIsDead)
            return;

        moveDir = callbackContext.ReadValue<Vector2>();
    }

    public void OnBulletDir(InputAction.CallbackContext callbackContext)
    {
        if (PlayerManager.Instance.playerIsDead)
            return;

        isShooting = charInput.BulletDir.IsPressed();

        var inputDir = callbackContext.ReadValue<Vector2>();

        if (inputDir == Vector2.zero)
            return;

        bulletDir = inputDir;
    }

    public void OnFireBomb(InputAction.CallbackContext callbackContext)
    {
        if (PlayerManager.Instance.playerIsDead || PlayerManager.Instance.BombItem <= 0)
            return;

        Debug.Log("ÆøÅº»ý¼º");

        ObjectManager.Instance.PoolingBombPrefab();
    }
}
