using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class IdleBodyState : PlayerBaseState
{
    public IdleBodyState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {// 상태 진입 시 실행
        base.Enter();
        Debug.Log("플레이어 대기");
    }

    public override void Update()
    {// 현재 상태일 동안 실행
        base.Update();

        if (PlayerManager.Instance.playerIsDead)
        {
            stateMachine.ChangedState(stateMachine.deadState);
            return;
        }

        stateMachine.Player.StopMovement();
        stateMachine.Player.HeadDirection(InputManager.Instance.IsShooting);

        if (InputManager.Instance.moveDir != Vector2.zero)
        {
            stateMachine.ChangedState(stateMachine.walkBodyState);
        }
    }

    public override void Exit()
    {// 상태를 벗어날때 실행
        base.Exit();
    }
}
