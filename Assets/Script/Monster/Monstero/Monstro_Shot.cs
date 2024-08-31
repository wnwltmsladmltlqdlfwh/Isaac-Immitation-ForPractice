using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstro_Shot : EnemyBaseState<MonstroCharacter>
{
    public Monstro_Shot(MonstroCharacter t_Enemy) : base(t_Enemy)
    {
        this.stateType = StateType.Pattern_One;
        animationHash = Animator.StringToHash("moveMonster");
    }

    protected override void StateStart()
    {
        throw new System.NotImplementedException();
    }

    protected override void StateUpdate()
    {
        throw new System.NotImplementedException();
    }
    protected override void StateExit()
    {
        throw new System.NotImplementedException();
    }
}
