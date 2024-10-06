using UnityEngine;

public class Monstro_Shot : EnemyBaseState<MonstroCharacter>
{
    float passedTime = 0f;

    public Monstro_Shot(MonstroCharacter t_Enemy) : base(t_Enemy)
    {
        this.stateType = StateType.Pattern_One;
        animationHash = Animator.StringToHash("MonstroShot");
    }

    protected override void StateStart()
    {
        _enemyChar.animator.SetTrigger(animationHash);
        passedTime = 0f;
    }

    protected override void StateUpdate()
    {
        passedTime += Time.deltaTime;
        if(passedTime > 1f)
        {
            _enemyChar.stateMachine.ChangeState(StateType.Idle);
        }
    }

    protected override void StateExit()
    {
        _enemyChar.shotPassTime = 8.0f;
        passedTime = 0f;
    }
}
