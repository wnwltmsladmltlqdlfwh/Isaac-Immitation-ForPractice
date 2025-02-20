using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerManager : Singleton<PlayerManager>
{
    private int maxHp;
    public int MaxHP
    {
        get { return maxHp; }
        set
        {
            if (maxHp != value)
            {
                maxHp = value;
            }
            if (curHp > maxHp)
            {
                curHp = maxHp;
            }
            OnHealthChanged?.Invoke();
        }
    }
    private int curHp;
    public int CurHP
    {
        get { return curHp; }
        set
        {
            if (curHp != value)
            {
                curHp = value;
                
                if (curHp == 0)
                {
                    playerIsDead = true;
                }

                if (curHp > maxHp)
                {
                    curHp = maxHp;
                }

                OnHealthChanged?.Invoke();
            }
        }
    }

    private int shiled;
    public int Shiled
    {
        get => shiled;
        set
        {
            if(shiled != value)
            {
                shiled = value;
                OnHealthChanged?.Invoke();
            }
        }
    }

    public event Action OnHealthChanged;

    public float AttackSpeed;
    public float AttackPower;
    public float AttackRange;
    public float BulletSpeed;

    public float MoveSpeed;
    public float Deceleration;

    public bool playerIsDead = false;

    private int bombItem;
    public int BombItem
    {
        get { return bombItem; }
        set
        {
            if(bombItem != value)
            {
                bombItem = value;
                OnCurrencyChanged?.Invoke();
            }
        }
    }

    private int moneyItem;
    public int MoneyItem
    {
        get { return moneyItem; }
        set
        {
            if(moneyItem != value)
            {
                moneyItem = value;
                OnCurrencyChanged?.Invoke();
            }
        }
    }

    private int keyItem;
    public int KeyItem
    {
        get => keyItem;
        set
        {
            if(keyItem != value)
            {
                keyItem = value;
                OnCurrencyChanged?.Invoke();
            }
        }
    }

    public event Action OnCurrencyChanged;

    private Dictionary<string, BulletBase> bulletDic = new Dictionary<string, BulletBase>();
    private Dictionary<string, SpriteLibraryAsset> bodySkinDic = new Dictionary<string, SpriteLibraryAsset>();
    private Dictionary<string, SpriteLibraryAsset> headSkinDic = new Dictionary<string, SpriteLibraryAsset>();
    public BulletBase currentBullet { get; private set; }

    public BulletExplodeEffect explodeEffect;

    public Dictionary<string, bool> bulletOptions = new Dictionary<string, bool>();

    private void Start()
    {
        InitBulletDic();
        InitSkinDic();
        InitBulletOptionsDic();
        SetCurrentBullet("DefalutBullet");

        BombItem = 0;

        BombItem += 10;

        KeyItem = 0;
        MoneyItem = 0;
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

    public void InitSkinDic()
    {
        var bodySkinArray = Resources.LoadAll<SpriteLibraryAsset>("SpriteLibrary/Body");
        var headSkinArray = Resources.LoadAll<SpriteLibraryAsset>("SpriteLibrary/Head");
        if (bodySkinArray == null)
        {
            Debug.Log("몸통 불러오기 실패");
            return;
        }

        for(int i = 0;i < bodySkinArray.Length; i++)
        {
            Debug.Log(bodySkinArray[i].name);
            bodySkinDic.Add(bodySkinArray[i].name, bodySkinArray[i]);
        }

        if (headSkinArray == null)
        {
            Debug.Log("머리 불러오기 실패");
            return;
        }

        for(int i = 0; i < headSkinArray.Length; i++)
        {
            Debug.Log(headSkinArray[i].name);
            headSkinDic.Add(headSkinArray[i].name, headSkinArray[i]);
        }
    }

    private void InitBulletOptionsDic()
    {
        bulletOptions.Add("Homing", false); //유도기능
        bulletOptions.Add("ThirdBullet", false); //3갈래 발사
    }

    public void SetCurrentBullet(string bulletName)
    {
        currentBullet = bulletDic[bulletName];
        Debug.Log($"현재 총알 : {currentBullet.name}");
    }

    public void ChangedPlayerSkin(string skin)
    {
        Debug.Log(skin);

        GameObject player = GameObject.FindWithTag("Player");
        var bodyL = player.transform.Find("Body").GetComponent<SpriteLibrary>();
        var headL = player.transform.Find("Head").GetComponent<SpriteLibrary>();

        if (bodySkinDic.ContainsKey($"{skin}Body") && headSkinDic.ContainsKey($"{skin}Head"))
        {
            bodyL.spriteLibraryAsset = bodySkinDic[$"{skin}Body"];
            headL.spriteLibraryAsset = headSkinDic[$"{skin}Head"];
        }
        else
        {
            Debug.Log("스킨 찾기 실패");
        }
    }
}
