using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Enter()
    {// 상태 진입 시 실행

    }

    public void Update()
    {// 업데이트에서 실행할 내용

    }

    public void Exit()
    {// 상태를 벗어날때 실행

    }

    public void HandleInput()
    {// 상태 머신 동작 중에 입력값 있을 시

    }

    public void PhysicsUpdate()
    {// 물리적 업데이트

    }
}
