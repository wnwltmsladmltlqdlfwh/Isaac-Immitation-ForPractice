using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WalkBodyState : PlayerBaseState
{
    public WalkBodyState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {// 상태 진입 시 실행
        Debug.Log("걷기 상태 진입");
        base.Enter();
    }

    public override void Update()
    {// 현재 상태일 동안 실행
        base.Update();

        var moveDir = InputManager.Instance.moveDir;

        stateMachine.Player.MoveCharactor(moveDir);
        stateMachine.Player.HeadDirection(InputManager.Instance.IsShooting);

        if(moveDir == Vector2.zero)
        {
            stateMachine.ChangedState(stateMachine.idleBodyState);
        }
    }

    public override void Exit()
    {// 상태를 벗어날때 실행
        Debug.Log("걷기 상태 벗어남");
        base.Exit();
    }
}
