using UnityEngine;

public class SpiderController : EnemyController
{
    void Start()
    {
        CurHealthPoint = 10f;
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
