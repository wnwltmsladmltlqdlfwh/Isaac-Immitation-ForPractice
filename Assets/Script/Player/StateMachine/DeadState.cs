using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : PlayerBaseState
{
    public DeadState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }
    public override void Enter()
    {// 상태 진입 시 실행
        base.Enter();
        Debug.Log("플레이어 사망");
        stateMachine.Player.PlayerDeadMotion();
    }

    public override void Update()
    {// 현재 상태일 동안 실행
        base.Update();

        stateMachine.Player.StopMovement();
    }

    public override void Exit()
    {// 상태를 벗어날때 실행
        base.Exit();
    }
}
