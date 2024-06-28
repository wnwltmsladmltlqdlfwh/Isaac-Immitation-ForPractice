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
    {// ���� ���� �� ����
        Debug.Log("��� ���� ����");
        base.Enter();
    }

    public override void Update()
    {// ���� ������ ���� ����
        base.Update();
        if(InputManager.Instance.moveDir != Vector2.zero)
        {
            stateMachine.ChangedState(stateMachine.walkBodyState);
        }
    }

    public override void Exit()
    {// ���¸� ����� ����
        Debug.Log("��� ���� ���");
        base.Exit();
    }
}
