using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public int MaxHP;
    public int CurHP;
    public int Shiled;

    public float AttackSpeed;
    public float AttackPower;
    public float AttackRange;
    public float BulletSpeed;

    public float MoveSpeed;
    public float Deceleration;

    public int BoomItem = 0;
    public int MoneyItem = 0;
    public int KeyItem = 0;

    public void InitPlayer(PlayerSO _Data)
    {
        MaxHP = _Data.playerConditionData.StartHealthPoint;
        CurHP = MaxHP;
        Shiled = _Data.playerConditionData.StartShiledPoint;

        AttackSpeed = _Data.playerAttackData.attackSpeed;
        AttackPower = _Data.playerAttackData.attackPower;
        AttackRange = _Data.playerAttackData.attackRange;
        BulletSpeed = _Data.playerAttackData.bulletSpeed;

        MoveSpeed = _Data.playerMovementData.moveSpeed;
        Deceleration = _Data.playerMovementData.deceleration;
    }
}
