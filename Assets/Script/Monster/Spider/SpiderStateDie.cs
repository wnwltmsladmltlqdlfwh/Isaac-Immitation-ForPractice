using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderStateDie : EnemyBaseState<SpiderCharacter>
{
    public SpiderStateDie(SpiderCharacter character) : base(character)
    {
        this.stateType = StateType.Die;
        animationHash = Animator.StringToHash("dieMonster");
    }

    protected override void StateStart()
    {
        _enemyChar.animator.SetTrigger(animationHash);
    }

    protected override void StateUpdate()
    {
        
    }

    protected override void StateExit()
    {

    }
}
