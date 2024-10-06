using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public enum ObjectType { None, Player, Enemy, Boss, PlayerBullet, EnemyBullet}

public class ObjectManager : Singleton<ObjectManager>
{
    // 몬스터 오브젝트(프리펩)관리 리스트
    public Dictionary<ObjectType, List<EnemyController>> enemyPrefabsList =
        new Dictionary<ObjectType, List<EnemyController>>();
    // 플레이어 오브젝트만을 관리하는 용도
    public PlayerController Player;

    public Bomb bombPrefab;
    public BombExplordeEffect bombExplordeEffect;

    [SerializeField] private List<PickUpItem> pickUpItemList;

    private void Awake()
    {
        InitThisManager();
    }

    private void InitThisManager()
    {
        var pickUpItemArray = Resources.LoadAll<PickUpItem>("Prefab/PickUpItems");
        if (pickUpItemArray == null) { return; }
        for (int i = 0; i < pickUpItemArray.Length; i++)
        {
            pickUpItemList.Add(pickUpItemArray[i]);
        }
    }

    public T_Enemy Spawn<T_Enemy>(T_Enemy objectPrefab, Vector3 postion = default, Quaternion rotaion = default) where T_Enemy : EnemyController
    {
        if(objectPrefab == null) return null;

        var obj = PoolingManager.Instance.Pop(objectPrefab);

        obj.transform.position = postion;
        obj.transform.rotation = rotaion;

        if (!enemyPrefabsList.ContainsKey(obj.objectType))
        {
            CreateList(obj.objectType);
        }

        enemyPrefabsList[obj.objectType].Add(obj);

        return obj;
    }

    public void Despawn<T_Enemy>(T_Enemy obj) where T_Enemy : EnemyController
    {
        enemyPrefabsList.Remove(obj.objectType);

        PoolingManager.Instance.Push(obj);
    }

    private void CreateList(ObjectType objectType)
    {
        var pool = new List<EnemyController>();
        enemyPrefabsList.Add(objectType, pool);
    }

    public void GetPlayerInfo(PlayerController player)
    {
        this.Player = player;
    }

    public Vector3 PlayerPosition()
    {
        return Player.gameObject.transform.position;
    }

    public void PoolingBombPrefab()
    {
        var pooledBomb = PoolingManager.Instance.Pop(bombPrefab);

        PlayerManager.Instance.BombItem -= 1;

        pooledBomb.transform.position = Player.transform.position;

        pooledBomb.Init();
    }

    public void BoomExplodeEffect(Vector3 effectPos)
    {
        var poolEffect = PoolingManager.Instance.Pop(bombExplordeEffect);
        poolEffect.transform.position = effectPos;
    }

    public void PoolingPickUpItems()
    {
        int makeItemCount = UnityEngine.Random.Range(1, 5);

        for (int i = 0; i < makeItemCount; i++)
        {
            int setItem = UnityEngine.Random.Range(0, pickUpItemList.Count);

            var pooledItem = PoolingManager.Instance.Pop(pickUpItemList[setItem]);

            Vector3 randPos = (Vector3)UnityEngine.Random.insideUnitCircle * 2f;

            pooledItem.transform.position = DungeonManager.Instance.currentRoom.transform.position + randPos;
        }
    }
}
