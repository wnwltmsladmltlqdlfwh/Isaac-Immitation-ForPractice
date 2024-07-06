using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private Dictionary<string, BulletBase> bulletDic = new Dictionary<string, BulletBase>();
    public BulletBase currentBullet { get; private set; }

    private void Start()
    {
        InitBulletDic();
        SetCurrentBullet("DefalutBullet");
    }

    public void InitPlayerData(PlayerSO _Data)
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

    public void InitBulletDic()
    {
        var bulletArray = Resources.LoadAll("Prefab/Bullet");
        if(bulletArray == null) { return; }
        for(int i = 0; i < bulletArray.Length; i++)
        {
            bulletDic.Add(bulletArray[i].name, bulletArray[i].GetComponent<BulletBase>());
            Debug.Log($"{bulletArray[i].name}");
        }
    }

    public void SetCurrentBullet(string bulletName)
    {
        currentBullet = bulletDic[bulletName];
        Debug.Log($"ÇöÀç ÃÑ¾Ë : {currentBullet.name}");
    }
}
