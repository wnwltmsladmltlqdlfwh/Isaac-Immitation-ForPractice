using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IState CurBodyState {  get; private set; }

    public IdleBodyState idlestate;
    public WalkBodyState walkstate;

    public event Action<IState> changedState;

    public StateMachine (PlayerController player)
    {
        this.idlestate = new IdleBodyState(player);
        this.walkstate = new WalkBodyState(player);
    }

    // 시작 시 상태 설정
    public void Initialize(IState state)
    {
        CurBodyState = state;
        state.Enter();

        changedState?.Invoke(state);
    }

    // 상태를 벗어날 때, 현재 상태를 변경해준다.
    public void TranslateTo(IState nextState)
    {
        CurBodyState.Exit();
        CurBodyState = nextState;
        nextState.Enter();

        changedState?.Invoke(nextState);
    }


    // 현재 상태의 Update 동작을 실행한다.
    public void Update()
    {
        if (CurBodyState != null)
        {
            CurBodyState.Update();
        }
    }
}
