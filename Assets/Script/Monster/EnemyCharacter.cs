using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EnemyCharacter : MonoBehaviour
{
    public EnemyController Controller;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    void Awake()
    {
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (Controller == null)
        {
            Controller = GetComponentInParent<EnemyController>();
        }

        Init();
    }

    protected virtual void Init()
    {

    }

    public void SetMoveDirection(Vector2 dir)
    {
        Controller.Move(dir);

        if (dir.x != 0)
            spriteRenderer.flipX = dir.x < 0;
    }

    public void SetMoveTarget(Vector3 targetPos)
    {
        var dir = targetPos - this.transform.position;

        SetMoveDirection(dir);
    }
}
