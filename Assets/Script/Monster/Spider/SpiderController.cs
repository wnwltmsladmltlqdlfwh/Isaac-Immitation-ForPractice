using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.VFX;

public class SpiderController : EnemyController
{
    void Start()
    {
        healthPoint = 2f;
    }

    public override void Move(Vector2 _dir)
    {
        base.Move(_dir);

        this.transform.Translate(moveDir * Time.deltaTime * moveSpeed);
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
    }

    public override void OnDie()
    {
        base.OnDie();

        ObjectManager.Instance.Despawn(this);
    }
}
