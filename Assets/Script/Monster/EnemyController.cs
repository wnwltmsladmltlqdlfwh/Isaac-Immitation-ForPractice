using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ObjectType objectType;

    public float moveSpeed = 10f;
    public Vector2 moveDir { get; private set; }

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

        // �̵��� �� ���Ϳ��� �߰�
    }

    public virtual void OnDamage()
    {

    }

    public virtual void OnDie()
    {
        ObjectManager.Instance.Despawn(this);
    }
}
