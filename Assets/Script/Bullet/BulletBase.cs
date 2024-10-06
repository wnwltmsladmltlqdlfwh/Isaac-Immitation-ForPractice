using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public Rigidbody2D _body2D;
    [field : SerializeField] public BulletSO Data { get; private set; }

    public Vector2 startPos;
    public Vector2 shootdir;
    private float passedTime = 0f;

    List<EnemyController> enemyList;

    private void Awake()
    {
        _body2D = GetComponent<Rigidbody2D>();
        enemyList = new List<EnemyController>();
    }

    private void Update()
    {
        passedTime += Time.deltaTime;

        float distance = Vector2.Distance(this.transform.localPosition, startPos);
        if(distance > PlayerManager.Instance.AttackRange)
        {
            ReturnBullet();
        }
        _body2D.velocity = shootdir * PlayerManager.Instance.BulletSpeed;
        ShotBullet();

        if (PlayerManager.Instance.bulletOptions["Homing"])
        {
            HomingProjectile();
        }
    }

    protected virtual void ShotBullet()
    {
        
    }

    public void Init(Vector2 inputShootDir, Vector3 pos)
    {
        this.transform.position = pos;

        Vector3 bulletScale =
            new Vector3 (PlayerManager.Instance.AttackPower, PlayerManager.Instance.AttackPower, PlayerManager.Instance.AttackPower);

        this.transform.localScale = Vector3.ClampMagnitude(bulletScale, 3f);

        startPos = pos;
        shootdir = inputShootDir;
        _body2D.AddForce(InputManager.Instance.moveDir * 10f, ForceMode2D.Impulse);

        if (PlayerManager.Instance.bulletOptions["Homing"])
        {
            EnemyController[] findEnemysArray = FindObjectsOfType<EnemyController>();
            enemyList.Clear();
            foreach (EnemyController enemy in findEnemysArray)
            {
                enemyList.Add(enemy);
            }
        }
    }

    private void HomingProjectile()
    {
        EnemyController closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (EnemyController enemy in enemyList)
        {
            if (enemy.isDead) { continue; }

            float distanceWithEnemy
                = Vector3.Distance(this.transform.position, enemy.transform.position);

            if (distanceWithEnemy <= 5f && distanceWithEnemy < closestDistance)
            {
                closestDistance = distanceWithEnemy;
                closestEnemy = enemy;
            };


            if (closestEnemy != null)
            {
                Vector3 homingDir = (closestEnemy.transform.position - transform.position).normalized;
                shootdir = Vector3.Lerp(shootdir, homingDir, Time.deltaTime * 5f).normalized;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().OnDamage(PlayerManager.Instance.AttackPower);

            if (!collision.gameObject.GetComponent<MonstroController>())
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - this.transform.position).normalized * 100f);
            }

            ReturnBullet();
        }

        if (collision.transform.CompareTag("Wall"))
        {
            ReturnBullet();
        }
    }

    private void ReturnBullet()
    {
        var effect = PoolingManager.Instance.Pop(PlayerManager.Instance.explodeEffect);
        effect.transform.position = this.transform.position;
        PoolingManager.Instance.Push(this);
    }
}
