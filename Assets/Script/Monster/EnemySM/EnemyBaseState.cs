using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class EnemyBaseState<T_Enemy> where T_Enemy : EnemyController
{
    public StateType stateType {  get; private set; }

    protected T_Enemy _enemy;
    protected int animationHash;

    public EnemyBaseState(T_Enemy t_Enemy)
    {
        this._enemy = t_Enemy;
    }

    public void OnStart()
    {
        StateStart();
    }

    public void OnUpdate()
    {
        StateUpdate();
    }

    public void OnExit()
    {
        StateExit();
    }

    protected abstract void StateStart();

    protected abstract void StateUpdate();

    protected abstract void StateExit();
}
