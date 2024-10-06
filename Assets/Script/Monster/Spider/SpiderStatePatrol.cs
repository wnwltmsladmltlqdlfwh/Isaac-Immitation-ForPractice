using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderStatePatrol : EnemyBaseState<SpiderCharacter>
{
    Vector3 randMove;

    public SpiderStatePatrol(SpiderCharacter character) : base(character)
    {
        this.stateType = StateType.Patrol;
        animationHash = Animator.StringToHash("moveMonster");
    }
    protected override void StateStart()
    {
        _enemyChar.animator.SetBool(animationHash, true);
        randMove = Random.insideUnitSphere;
        statePassTime = 0.5f;
    }

    protected override void StateUpdate()
    {
        var distanceWithPlayer = (ObjectManager.Instance.PlayerPosition() - _enemyChar.transform.position).sqrMagnitude;


        if (distanceWithPlayer <= _enemyChar.ChasedPlayerDistance * _enemyChar.ChasedPlayerDistance)
        {
            _enemyChar.stateMachine.ChangeState(StateType.Chase);
        }
        else
        {
            statePassTime -= Time.deltaTime;

            _enemyChar.SetMoveDirection(randMove);

            if (statePassTime <= 0f)
            {
                _enemyChar.stateMachine.ChangeState(StateType.Idle);
            }
        }
    }

    protected override void StateExit()
    {
        
    }
}
