using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Enter()
    {// 상태 진입 시 실행

    }

    public void Update()
    {// 현재 상태일 동안 실행

    }

    public void Exit()
    {// 상태를 벗어날때 실행

    }
}
