using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class EnemyBaseState<T_Enemy> where T_Enemy : EnemyCharacter
{
    public StateType stateType { get; protected set; }

    protected T_Enemy _enemyChar;
    protected int animationHash;
    protected float statePassTime;

    public EnemyBaseState(T_Enemy t_Enemy)
    {
        this._enemyChar = t_Enemy;
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
