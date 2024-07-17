using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateMachine <T> : StateMachine where T : MonsterController
{
    public T Monster { get; }

    //����
    public MonsterIdleState monsterIdleState;
    public MonsterMoveState monsterMoveState;
    public MonsterChaseState monsterChaseState;
    public MonsterDieState monsterDieState;

    //�̵� ����
    public Vector2 MovementInput { get; set; }
    public float MoveSpeed { get; private set; }

    public MonsterStateMachine(T moster) 
    {
        this.Monster = moster;

        monsterIdleState = new MonsterIdleState(this);
        monsterMoveState = new MonsterMoveState(this);
        monsterChaseState = new MonsterChaseState(this);
        monsterDieState = new MonsterDieState(this);

        //MoveSpeed = player.Data.playerMovementData.moveSpeed;
    }
}
