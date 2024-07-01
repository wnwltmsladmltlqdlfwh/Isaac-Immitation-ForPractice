using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DefaultBullet : MonoBehaviour
{
    public IObjectPool<GameObject> Pool { get; set; }
    private Rigidbody2D _body2D;
    [field : SerializeField] public BulletSO Data { get; private set; }

    public Vector2 startPos;
    public Vector2 shootdir;

    private void OnEnable()
    {
        startPos = this.transform.position;
        shootdir = InputManager.Instance.bulletDir;
        _body2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float distance = Vector2.Distance(this.transform.localPosition, startPos);
        if(distance > Data.bulletData.bulletRange)
        {
            Pool.Release(this.gameObject);
        }

        this.transform.Translate(shootdir * Data.bulletData.bulletSpeed * Time.deltaTime);
        //_body2D.velocity += shootdir * Data.bulletData.bulletSpeed * Time.deltaTime;
    }
}
