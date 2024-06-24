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

    // ���� �� ���� ����
    public void Initialize(IState state)
    {
        CurBodyState = state;
        state.Enter();

        changedState?.Invoke(state);
    }

    // ���¸� ��� ��, ���� ���¸� �������ش�.
    public void TranslateTo(IState nextState)
    {
        CurBodyState.Exit();
        CurBodyState = nextState;
        nextState.Enter();

        changedState?.Invoke(nextState);
    }


    // ���� ������ Update ������ �����Ѵ�.
    public void Update()
    {
        if (CurBodyState != null)
        {
            CurBodyState.Update();
        }
    }
}
