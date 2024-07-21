using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderCharacter : EnemyCharacter
{
    public EnemyStateMachine<SpiderCharacter> stateMachine { get; private set; }

    protected override void Init()
    {
        base.Init();

        stateMachine = new EnemyStateMachine<SpiderCharacter>();
        stateMachine.AddStateList(new SpiderStateMove(this));
    }
}
