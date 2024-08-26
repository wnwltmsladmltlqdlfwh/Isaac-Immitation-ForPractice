using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{ // 사용할(만들) 몬스터의 State
    Idle,
    Patrol,
    Chase,
    Die
}

public class EnemyStateMachine<T_Enemy> where T_Enemy : EnemyCharacter
{
    public EnemyBaseState<T_Enemy> currentState;

    private Dictionary<StateType, EnemyBaseState<T_Enemy>> EnemyStateList = new Dictionary<StateType, EnemyBaseState<T_Enemy>>();

    public void Update()
    {
        if (currentState == null) { return; }

        currentState.OnUpdate();
    }

    public void AddStateList(EnemyBaseState<T_Enemy> state)
    {
        if (currentState == null)
        {
            currentState = state;
        }

        EnemyStateList.Add(state.stateType, state);
    }

    public void ChangeState(StateType stateType)
    {
        if (!EnemyStateList.ContainsKey(stateType))
        {
            Debug.Log("State is Null" + stateType.ToString());
        }

        currentState?.OnExit();
        currentState = EnemyStateList[stateType];
        currentState?.OnStart();
    }
}
