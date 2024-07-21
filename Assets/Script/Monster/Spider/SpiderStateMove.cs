using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderStateMove : EnemyBaseState<SpiderCharacter>
{
    public SpiderStateMove(SpiderCharacter character) : base(character)
    {
        this.stateType = StateType.Move;
    }

    protected override void StateExit()
    {
        throw new System.NotImplementedException();
    }

    protected override void StateStart()
    {
        throw new System.NotImplementedException();
    }

    protected override void StateUpdate()
    {
        throw new System.NotImplementedException();
    }
}
