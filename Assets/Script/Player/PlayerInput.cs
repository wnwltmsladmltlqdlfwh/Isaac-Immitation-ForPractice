using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputSystem playerInputSystem { get; private set; }
    public PlayerInputSystem.CharactorActionsMapActions charInput { get; private set; }

    private void Awake()
    {
        playerInputSystem = new PlayerInputSystem();

        charInput = playerInputSystem.CharactorActionsMap;
    }

    private void OnEnable()
    {
        playerInputSystem.Enable();
    }

    private void OnDisable()
    {
        playerInputSystem.Disable();
    }
}
