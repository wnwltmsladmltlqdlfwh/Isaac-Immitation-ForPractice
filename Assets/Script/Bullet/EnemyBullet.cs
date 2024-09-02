using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Rigidbody2D _body2D;
    [field: SerializeField] public BulletSO Data { get; private set; }

    public Vector2 shootdir;
    private float passedTime = 0f;

    private void Awake()
    {
        _body2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        passedTime += Time.deltaTime;

        if (1f < passedTime)
        {
            ReturnBullet();
        }
        _body2D.velocity = shootdir * Random.Range(0.5f, 2f);
    }

    public void Init(Vector2 playerPos, Vector2 thisPos)
    {
        passedTime = 0f;

        this.transform.position = thisPos;

        float size = Random.Range(0.5f, 1.5f);

        this.transform.localScale =
            new Vector2(size, size);

        shootdir =
            (playerPos + Random.insideUnitCircle * 3f) - thisPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            collision.gameObject.GetComponent<PlayerController>().OnDamaged();

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
