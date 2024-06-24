using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBodyState : IState
{
    public PlayerController player;


    public IdleBodyState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {// ���� ���� �� ����
        Debug.Log("��� ���� ����");
    }

    public void Update()
    {// ���� ������ ���� ����

    }

    public void Exit()
    {// ���¸� ����� ����
        Debug.Log("��� ���� ���");
    }
}
