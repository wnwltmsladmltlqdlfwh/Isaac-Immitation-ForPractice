using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstroController : EnemyController
{
    public float MaxHealthPoint;

    void Start()
    {
        CurHealthPoint = 250f;

        MaxHealthPoint = CurHealthPoint;
    }

    private void Update()
    {

    }

    public override void Move(Vector2 _dir)
    {
        base.Move(_dir);

        this.transform.Translate(moveDir * Time.deltaTime * moveSpeed);
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);

        if(InGameUIManager.Instance.bossHealthUI != null)
        {
            InGameUIManager.Instance.BossUIUpdate(CurHealthPoint, MaxHealthPoint);
        }
    }

    public override void OnDie()
    {
        base.OnDie();

        if (InGameUIManager.Instance.bossHealthUI != null)
        {
            InGameUIManager.Instance.BossBattleUI(false);
        }

        DungeonManager.Instance.bossRoom.OpenEndDoor();

        ObjectManager.Instance.Despawn(this);
    }
}
