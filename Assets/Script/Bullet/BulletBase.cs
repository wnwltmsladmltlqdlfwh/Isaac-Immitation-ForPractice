using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletBase : MonoBehaviour
{
    public Rigidbody2D _body2D;
    [field : SerializeField] public BulletSO Data { get; private set; }

    public Vector2 startPos;
    public Vector2 shootdir;
    private float passedTime = 0f;

    private void Awake()
    {
        _body2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        passedTime += Time.deltaTime;

        float distance = Vector2.Distance(this.transform.localPosition, startPos);
        if(distance > PlayerManager.Instance.AttackRange) //Data.bulletData.bulletRange
        {
            PoolingManager.Instance.Push(this);
        }
        ShotBullet();
    }

    protected virtual void ShotBullet()
    {
        _body2D.velocity = shootdir * PlayerManager.Instance.BulletSpeed;
    }

    public void Init(Vector2 inputShootDir, Vector3 pos)
    {
        this.transform.position = pos;
        startPos = pos;
        shootdir = inputShootDir;
    }
}
