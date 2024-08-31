using System.Collections;

public class MonstroCharacter : EnemyCharacter
{
    public EnemyStateMachine<MonstroCharacter> stateMachine { get; private set; }

    protected override void Init()
    {
        base.Init();

        stateMachine = new EnemyStateMachine<MonstroCharacter>();
        stateMachine.AddStateList(new Monstro_Idle(this));
        stateMachine.AddStateList(new Monstro_Chase(this));
        stateMachine.AddStateList(new Monstro_Shot(this));
        stateMachine.AddStateList(new Monstro_Stamp(this));
    }

    private void Update()
    {
        if (PlayerManager.Instance.playerIsDead) return;

        stateMachine.Update();
    }
}
