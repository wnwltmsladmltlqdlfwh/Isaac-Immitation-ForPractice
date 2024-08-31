using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstro_Idle : EnemyBaseState<MonstroCharacter>
{
    public Monstro_Idle(MonstroCharacter t_Enemy) : base(t_Enemy)
    {
        this.stateType = StateType.Idle;
        animationHash = Animator.StringToHash("moveMonster");
    }

    protected override void StateStart()
    {
    }

    protected override void StateUpdate()
    {
        
    }

    protected override void StateExit()
    {
    }
}
