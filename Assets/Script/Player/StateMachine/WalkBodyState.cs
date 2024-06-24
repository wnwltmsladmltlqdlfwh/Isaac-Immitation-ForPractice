using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBodyState : IState
{
    public PlayerController player;


    public WalkBodyState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {// ���� ���� �� ����
        Debug.Log("�ȱ� ���� ����");
    }

    public void Update()
    {// ���� ������ ���� ����

    }

    public void Exit()
    {// ���¸� ����� ����
        Debug.Log("�ȱ� ���� ���");
    }
}
