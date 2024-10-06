using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderStateIdle : EnemyBaseState<SpiderCharacter>
{
    public SpiderStateIdle(SpiderCharacter character) : base(character)
    {
        this.stateType = StateType.Idle;
        animationHash = Animator.StringToHash("moveMonster");
    }

    protected override void StateStart()
    {
        _enemyChar.animator.SetBool(animationHash, false);
        statePassTime = 0.5f;
    }

    protected override void StateUpdate()
    {
        var distanceWihtPlayer = (ObjectManager.Instance.PlayerPosition() - _enemyChar.transform.position).sqrMagnitude;


        if(distanceWihtPlayer <= _enemyChar.ChasedPlayerDistance * _enemyChar.ChasedPlayerDistance)
        {
            _enemyChar.stateMachine.ChangeState(StateType.Chase);
        }
        else
        {
            statePassTime -= Time.deltaTime;

            if (statePassTime <= 0f)
            {
                _enemyChar.stateMachine.ChangeState(StateType.Patrol);
            }
        }
    }

    protected override void StateExit()
    {

    }
}