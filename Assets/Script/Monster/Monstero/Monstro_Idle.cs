using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstro_Idle : EnemyBaseState<MonstroCharacter>
{
    float passedTime = 0f;

    public Monstro_Idle(MonstroCharacter t_Enemy) : base(t_Enemy)
    {
        this.stateType = StateType.Idle;
        //animationHash = Animator.StringToHash("moveMonster");
    }

    protected override void StateStart()
    {
        passedTime = 0f;
    }

    protected override void StateUpdate()
    {
        passedTime += Time.deltaTime;

        if (passedTime < 1f)
            return;

        if(_enemyChar.shotPassTime <= 0)
        {
            _enemyChar.stateMachine.ChangeState(StateType.Pattern_One);
        }
        else if(_enemyChar.stampPassTime <= 0)
        {
            _enemyChar.stateMachine.ChangeState(StateType.Pattern_Two);
        }
        else
        {
            _enemyChar.stateMachine.ChangeState(StateType.Chase);
        }
    }

    protected override void StateExit()
    {
        passedTime = 0f;
    }
}
