using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstro_Drop : EnemyBaseState<MonstroCharacter>
{
    Vector3 startPos;
    Vector3 targetPos;

    float moveTime = 0f;
    float startTime = 0f;

    public Monstro_Drop(MonstroCharacter t_Enemy) : base(t_Enemy)
    {
        this.stateType = StateType.Pattern_Three;
        animationHash = Animator.StringToHash("MonstroDrop");
    }


    protected override void StateStart()
    {
        startPos = _enemyChar.Controller.transform.position;
        targetPos = ObjectManager.Instance.PlayerPosition();

        moveTime = 4f;
    }

    protected override void StateUpdate()
    {
        DropDown();
    }

    void DropDown()
    {
        if (startTime < moveTime)
        {
            startTime += Time.deltaTime;
            float fractionOfJourney = startTime / moveTime;

            _enemyChar.Controller.transform.position = Vector3.Lerp(startPos, targetPos, fractionOfJourney);
        }
        else
        {
            _enemyChar.animator.SetTrigger(animationHash);
            _enemyChar.stateMachine.ChangeState(StateType.Idle);
        }
    }

    protected override void StateExit()
    {
        throw new System.NotImplementedException();
    }
}
