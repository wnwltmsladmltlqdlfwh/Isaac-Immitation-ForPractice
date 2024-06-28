using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field : Header("Animations")]
    [field : SerializeField] public PlayerAnimation AnimationData { get; private set; }

    [field : Header("References")]
    [field : SerializeField] public PlayerSO Data { get; private set; }

    public Rigidbody Rigidbody { get; private set; }

    public Animator HeadAnimator;
    public Animator BodyAnimator;
    public CharacterController Controller { get; private set; }


    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        stateMachine = new PlayerStateMachine(this);
        AnimationData.Initialize();
    }

    void Start()
    {
        stateMachine.ChangedState(stateMachine.idleBodyState);
    }

    void Update()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {

    }

    public void MoveCharactor(Vector2 moveVector)
    {
        transform.Translate(moveVector * Data.playerMovementData.moveSpeed * Time.deltaTime);

        BodyAnimator.SetFloat(AnimationData.inputXParameterHash, moveVector.x);
        BodyAnimator.SetFloat(AnimationData.inputYParameterHash, moveVector.y);
    }
}
