using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstro_Chase : EnemyBaseState<MonstroCharacter>
{
    public Monstro_Chase(MonstroCharacter t_Enemy) : base(t_Enemy)
    {
        this.stateType = StateType.Chase;
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
