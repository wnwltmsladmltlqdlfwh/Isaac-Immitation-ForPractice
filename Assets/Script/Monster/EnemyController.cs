using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ObjectType objectType;

    public float moveSpeed = 1f;
    public Vector2 moveDir { get; private set; }

    public bool isDead = false;

    public float CurHealthPoint;

    private void Awake()
    {

    }

    public virtual void Init()
    {
        objectType = ObjectType.Enemy;
    }

    public virtual void Move(Vector2 _dir)
    {
        moveDir = _dir;
    }

    public virtual void OnDamage(float damage)
    {
        CurHealthPoint -= damage; //PlayerManager.Instance.AttackPower;

        if (CurHealthPoint <= 0)
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
