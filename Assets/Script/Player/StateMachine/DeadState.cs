using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : PlayerBaseState
{
    public DeadState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }
    public override void Enter()
    {// ���� ���� �� ����
        base.Enter();
        Debug.Log("�÷��̾� ���");
        stateMachine.Player.PlayerDeadMotion();
    }

    public override void Update()
    {// ���� ������ ���� ����
        base.Update();

        stateMachine.Player.StopMovement();
    }

    public override void Exit()
    {// ���¸� ����� ����
        base.Exit();
    }
}
