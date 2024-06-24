using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    StateMachine stateMachine;

    public PlayerInput input { get; private set; }
    public CharacterController characterController { get; private set; }

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();

        stateMachine = new StateMachine(this);
    }

    void Start()
    {
        stateMachine.Initialize(stateMachine.idlestate);
    }

    void Update()
    {
        stateMachine.Update();
    }

    public void Move(Vector2 moveVec)
    {
        
    }
}
