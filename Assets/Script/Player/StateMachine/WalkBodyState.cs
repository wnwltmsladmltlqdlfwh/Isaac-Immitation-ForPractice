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
    {// ���� ���� �� ����
        Debug.Log("�ȱ� ���� ����");
        base.Enter();
    }

    public override void Update()
    {// ���� ������ ���� ����
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
    {// ���¸� ����� ����
        Debug.Log("�ȱ� ���� ���");
        base.Exit();
    }
}
