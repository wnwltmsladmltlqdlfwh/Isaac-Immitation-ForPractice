using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public enum ObjectType { None, Player, Enemy, Boss, PlayerBullet, EnemyBullet}

public class ObjectManager : Singleton<ObjectManager>
{
    // ���� ������Ʈ(������)���� ����Ʈ
    public Dictionary<ObjectType, List<EnemyController>> enemyPrefabsList =
        new Dictionary<ObjectType, List<EnemyController>>();
    // �÷��̾� ������Ʈ���� �����ϴ� �뵵
    PlayerController Player;

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

    public Vector2 PlayerPosition()
    {
        return Player.gameObject.transform.position;
    }
}
