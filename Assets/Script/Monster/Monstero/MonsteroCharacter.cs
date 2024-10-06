using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MonstroCharacter : EnemyCharacter
{
    public EnemyStateMachine<MonstroCharacter> stateMachine { get; private set; }

    public float shotPassTime;
    public float stampPassTime;

    protected override void Init()
    {
        base.Init();

        shotPassTime = 8.0f;
        stampPassTime = 10.0f;

        stateMachine = new EnemyStateMachine<MonstroCharacter>();
        stateMachine.AddStateList(new Monstro_Idle(this));
        stateMachine.AddStateList(new Monstro_Chase(this));
        stateMachine.AddStateList(new Monstro_Shot(this));
        stateMachine.AddStateList(new Monstro_Stamp(this));
        stateMachine.AddStateList(new Monstro_Drop(this));
    }

    private void Update()
    {
        if (PlayerManager.Instance.playerIsDead) return;

        if (shotPassTime > 0)
        {
            shotPassTime -= Time.deltaTime;
        }

        if (stampPassTime > 0)
        {
            stampPassTime -= Time.deltaTime;
        }

        if(this.transform.localPosition.y > 2f)
        {
            Controller.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            Controller.GetComponent<Collider2D>().enabled = true;
        }

        stateMachine.Update();
    }

    public void MonstroShotBullet(int count)
    {
        for(int i = 0; i < count; i++)
        {
            var bullet = PoolingManager.Instance.Pop(PoolingManager.Instance.enemyBullet);

            bullet.Init((Vector2)ObjectManager.Instance.PlayerPosition(), Controller.transform.position);
        }
    }

    public void MonstroChasePlayer()
    {
        var dir = (ObjectManager.Instance.PlayerPosition() - Controller.transform.position).normalized;

        if (dir.x != 0)
            spriteRenderer.flipX = dir.x < 0;

        _= StartCoroutine(MoveMonstro(dir));
    }

    private IEnumerator MoveMonstro(Vector3 dir)
    {
        Vector3 startPosition = Controller.transform.position;

        Vector3 targetPosition = startPosition + dir * 3f;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            float t = elapsedTime / 1f;
            Controller.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
