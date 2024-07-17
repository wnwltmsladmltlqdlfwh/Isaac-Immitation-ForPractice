using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterBaseState : IState
{
    protected MonsterStateMachine<MonsterController> stateMachine;

    public MonsterBaseState(MonsterStateMachine<MonsterController> monsterStateMachine)
    {
        stateMachine = monsterStateMachine;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void Update()
    {

    }

    protected virtual void AddInputActionsCallbacks()
    {

    }

    protected virtual void RemoveInputActionsCallbacks()
    {

    }
}
