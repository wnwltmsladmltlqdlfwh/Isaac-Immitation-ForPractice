using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public MonsterStateMachine MonsterStateMachine;
    public float passedTime = 0f;
    public Animator animator;
    public Rigidbody2D rb2D;

    public float ChaseRange;

    private void Awake()
    {
        MonsterStateMachine = new MonsterStateMachine(this);
        rb2D = GetComponent<Rigidbody2D>();
    }
}
