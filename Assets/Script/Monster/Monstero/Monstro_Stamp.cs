using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstro_Stamp : EnemyBaseState<MonstroCharacter>
{
    float startTime = 0f;
    float moveTime = 1f;

    public Monstro_Stamp(MonstroCharacter t_Enemy) : base(t_Enemy)
    {
        this.stateType = StateType.Pattern_Two;
        animationHash = Animator.StringToHash("MonstroStamp");
    }

    protected override void StateStart()
    {
        _enemyChar.animator.SetTrigger(animationHash);

        startTime = 0f;

        moveTime = 1f;
    }

    protected override void StateUpdate()
    {
        if (startTime < moveTime)
        {
            startTime += Time.deltaTime;
            return;
        }

        _enemyChar.stateMachine.ChangeState(StateType.Pattern_Three);
    }

    protected override void StateExit()
    {
    }
}
