using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ObjectType objectType;

    public float moveSpeed = 1f;
    public Vector2 moveDir { get; private set; }

    public bool isDead = false;

    public float healthPoint;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        objectType = ObjectType.Enemy;
    }

    public virtual void Move(Vector2 _dir)
    {
        moveDir = _dir;
    }

    public virtual void OnDamage(float damage)
    {
        healthPoint -= damage; //PlayerManager.Instance.AttackPower;

        if (healthPoint <= 0)
        {
            OnDie();
        }
    }

    public virtual void OnDie()
    {
        isDead = true;

        if (GameManager.Instance.CurRoomMonsterCount > 0)
        {
            GameManager.Instance.CurRoomMonsterCount--;
        }
    }
}
