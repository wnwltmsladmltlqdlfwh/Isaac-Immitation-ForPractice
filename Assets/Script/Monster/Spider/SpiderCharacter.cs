using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderCharacter : EnemyCharacter
{
    public EnemyStateMachine<SpiderCharacter> stateMachine { get; private set; }

    public float ChasedPlayerDistance;

    protected override void Init()
    {
        base.Init();

        stateMachine = new EnemyStateMachine<SpiderCharacter>();
        stateMachine.AddStateList(new SpiderStatePatrol(this));
        stateMachine.AddStateList(new SpiderStateIdle(this));
        stateMachine.AddStateList(new SpiderStateChase(this));
        stateMachine.AddStateList(new SpiderStateDie(this));
    }

    private void Update()
    {
        if(PlayerManager.Instance.playerIsDead) return;

        stateMachine.Update();
    }
}
