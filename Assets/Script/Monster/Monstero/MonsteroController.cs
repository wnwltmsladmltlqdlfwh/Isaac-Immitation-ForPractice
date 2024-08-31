using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstroController : EnemyController
{
    public float shotPassTime;
    public float stampPassTime;

    void Start()
    {
        healthPoint = 250f;
        shotPassTime = 8.0f;
        stampPassTime = 10.0f;
    }

    private void Update()
    {
        if(shotPassTime > 0)
        {
            shotPassTime -= Time.deltaTime;
        }

        if (stampPassTime > 0)
        {
            stampPassTime -= Time.deltaTime;
        }
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
