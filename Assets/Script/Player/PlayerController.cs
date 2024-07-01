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

    public Rigidbody2D rb2D { get; private set; }

    public Animator HeadAnimator;
    public Animator BodyAnimator;


    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        stateMachine = new PlayerStateMachine(this);
        rb2D = GetComponent<Rigidbody2D>();


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

    public void MoveCharactor(Vector2 moveDir)
    {
        var movementData = Data.playerMovementData;

        rb2D.AddForce(moveDir * movementData.moveSpeed);

        BodyAnimator.SetFloat(AnimationData.inputXParameterHash, moveDir.x);
        BodyAnimator.SetFloat(AnimationData.inputYParameterHash, moveDir.y);
    }

    public void StopMovement()
    {
        var movementData = Data.playerMovementData;

        // 현재 속도
        Vector2 curVelocity = rb2D.velocity;

        Vector2 newVelocity = Vector2.Lerp(curVelocity, Vector2.zero, movementData.deceleration * Time.deltaTime);

        rb2D.velocity = newVelocity;
    }

    public void HeadDirection(bool isShooting)
    {
        HeadAnimator.SetBool(AnimationData.inputShootHash, isShooting);

        if (isShooting)
        {
            HeadAnimator.speed = Data.playerAttackData.attackSpeed;
            HeadAnimator.SetFloat(AnimationData.inputXBulletHash, InputManager.Instance.bulletDir.x);
            HeadAnimator.SetFloat(AnimationData.inputYBulletHash, InputManager.Instance.bulletDir.y);
        }
        else
        {
            HeadAnimator.SetFloat(AnimationData.inputXHeadHash, InputManager.Instance.moveDir.x);
            HeadAnimator.SetFloat(AnimationData.inputYHeadHash, InputManager.Instance.moveDir.y);
        }
    }
}
