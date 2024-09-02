using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstro_Chase : EnemyBaseState<MonstroCharacter>
{
    float passedTime;

    public Monstro_Chase(MonstroCharacter t_Enemy) : base(t_Enemy)
    {
        this.stateType = StateType.Chase;
        animationHash = Animator.StringToHash("MonstroChase");
    }

    protected override void StateStart()
    {
        passedTime = 0f;
        _enemyChar.animator.SetTrigger(animationHash);
    }

    protected override void StateUpdate()
    {
        passedTime += Time.deltaTime;
        if(passedTime > 1f)
        {
            _enemyChar.stateMachine.ChangeState(StateType.Idle);
        }
    }

    protected override void StateExit()
    {
        passedTime = 0f;
    }


}
