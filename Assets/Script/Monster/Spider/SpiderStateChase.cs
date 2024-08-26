using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderStateChase : EnemyBaseState<SpiderCharacter>
{
    public SpiderStateChase(SpiderCharacter character) : base(character)
    {
        this.stateType = StateType.Chase;
        animationHash = Animator.StringToHash("moveMonster");
        statePassTime = 1f;
    }
    
    private bool isChased = false;

    protected override void StateStart()
    {
    }

    protected override void StateUpdate()
    {
        statePassTime -= Time.deltaTime;

        if(statePassTime < 0f)
        {
            statePassTime = 0.5f;
            isChased = !isChased;
        }

        _enemyChar.animator.SetBool(animationHash, !isChased);

        if (isChased)
        {
            return;
        }

        var distanceWihtPlayer = (ObjectManager.Instance.PlayerPosition() - _enemyChar.transform.position).sqrMagnitude;

        var playerPos = ObjectManager.Instance.PlayerPosition() - _enemyChar.transform.position;

        if (distanceWihtPlayer >= _enemyChar.ChasedPlayerDistance * _enemyChar.ChasedPlayerDistance)
        {
            _enemyChar.stateMachine.ChangeState(StateType.Idle);
        }
        else
        {
            _enemyChar.SetMoveDirection(playerPos.normalized);
        }
    }

    protected override void StateExit()
    {

    }
}
