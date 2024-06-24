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
    {// 상태 진입 시 실행
        Debug.Log("대기 상태 진입");
    }

    public void Update()
    {// 현재 상태일 동안 실행

    }

    public void Exit()
    {// 상태를 벗어날때 실행
        Debug.Log("대기 상태 벗어남");
    }
}
