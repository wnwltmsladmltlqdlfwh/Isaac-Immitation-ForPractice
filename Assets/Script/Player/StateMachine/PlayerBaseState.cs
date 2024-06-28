using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerMovementData playerMovementData;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        playerMovementData = stateMachine.Player.Data.playerMovementData;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void Update()
    {

    }

    protected virtual void AddInputActionsCallbacks()
    {

    }

    protected virtual void RemoveInputActionsCallbacks()
    {

    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.HeadAnimator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.HeadAnimator.SetBool(animationHash, false);
    }
}
