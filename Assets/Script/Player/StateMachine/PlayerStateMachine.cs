using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public PlayerController Player { get; }

    //����
    public IdleBodyState idleBodyState;
    public WalkBodyState walkBodyState;

    //�̵� ����
    public Vector2 MovementInput { get; set; }
    public float MoveSpeed { get; private set; }

    public PlayerStateMachine(PlayerController player)
    {
        this.Player = player;

        idleBodyState = new IdleBodyState(this);
        walkBodyState = new WalkBodyState(this);

        MoveSpeed = player.Data.playerMovementData.moveSpeed;
    }
}
