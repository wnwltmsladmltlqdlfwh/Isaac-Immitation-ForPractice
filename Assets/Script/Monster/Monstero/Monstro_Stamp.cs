using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstro_Stamp : EnemyBaseState<MonstroCharacter>
{
    float moveTime = 0f;
    float startTime = 0f;

    public Monstro_Stamp(MonstroCharacter t_Enemy) : base(t_Enemy)
    {
        this.stateType = StateType.Pattern_Two;
        animationHash = Animator.StringToHash("MonstroStamp");
    }

    protected override void StateStart()
    {
        _enemyChar.animator.SetTrigger(animationHash);

        moveTime = 1f;
    }

    protected override void StateUpdate()
    {
        RiseUp();
    }

    void RiseUp()
    {
        if (startTime < moveTime)
        {
            startTime += Time.deltaTime;
        }
        else
        {
            _enemyChar.stateMachine.ChangeState(StateType.Pattern_Three);
        }
    }

    protected override void StateExit()
    {
        throw new System.NotImplementedException();
    }
}