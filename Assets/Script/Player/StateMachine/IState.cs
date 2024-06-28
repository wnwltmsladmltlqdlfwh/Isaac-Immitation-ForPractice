using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Enter()
    {// ���� ���� �� ����

    }

    public void Update()
    {// ������Ʈ���� ������ ����

    }

    public void Exit()
    {// ���¸� ����� ����

    }

    public void HandleInput()
    {// ���� �ӽ� ���� �߿� �Է°� ���� ��

    }

    public void PhysicsUpdate()
    {// ������ ������Ʈ

    }
}
