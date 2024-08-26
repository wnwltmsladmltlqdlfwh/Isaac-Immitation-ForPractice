using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DungeonManager : Singleton<DungeonManager>
{
    public Room[,] dungeonSize;
    public Room currentRoom;
    private List<Room> dungeonList = new List<Room>();

    [SerializeField] private int maxXValue;
    [SerializeField] private int maxYValue;
    [SerializeField] private Room RoomPrefab;
    [SerializeField] private int disconnectedRoomCount;

    public int makeDungeonRoomsCount;
    public int goldRoomsCount;
    public int SpawnMonsterRoomsCount;

    public Action onRoomChange;

    //Transform roomsParent;

    System.Random rand = new System.Random();


    public GameObject testHomingItem;
    public GameObject testSkinItem;

    private void Awake()
    {
        dungeonSize = new Room[maxXValue, maxYValue];

        //if (roomsParent == null)
        //{
        //    var parent = Instantiate(new GameObject());
        //    parent.name = "RoomParent";
        //    parent.transform.position = Vector3.zero;
        //    roomsParent = parent.transform;
        //}
    }

    private void OnEnable()
    {
        MakeDungeonPreafabs();
    }

    private void Start()
    {
        StartCoroutine(SetUsedRoom());
    }

    public void MakeDungeonPreafabs()
    {
        if (RoomPrefab == null) { return; }

        for (int i = 0; i < maxXValue; i++)
        {
            for (int j = 0; j < maxYValue; j++)
            {
                //var roomPrefab = Instantiate(RoomPrefab, roomsParent);
                var roomPrefab = PoolingManager.Instance.Pop(RoomPrefab);
                roomPrefab.name = $"room({i}, {j})";
                dungeonSize[i, j] = roomPrefab;
                roomPrefab.InitRoomArrayPos(i, j);

                if (i == (maxXValue / 2) & j == (maxYValue / 2))
                {
                    dungeonSize[i, j].IsStartRoom();
                    currentRoom = dungeonSize[i, j];
                    dungeonList.Add(dungeonSize[i, j]);
                }
            }
        }

        Debug.Log("������ ������ �������");
        if (testHomingItem != null)
        {
            testHomingItem.transform.position =
                new Vector2(currentRoom.transform.position.x + 3, currentRoom.transform.position.y + 3);
        }

        if (testSkinItem != null)
        {
            testSkinItem.transform.position =
                new Vector2(currentRoom.transform.position.x - 3, currentRoom.transform.position.y + 3);
        }
    }

    public IEnumerator SetUsedRoom()
    {
        while (makeDungeonRoomsCount > 0)
        {
            // ���� �濡 ���� ������ ������ List
            List<Room> aroundRooms = new List<Room>();

            // ���� ������� ���� List���� 4���⿡ �ִ� ����� ���� ��������
            foreach (Room usingRoom in dungeonList)
            {
                if(usingRoom.roomPosX + 1 < maxXValue &&
                    !dungeonSize[usingRoom.roomPosX + 1, usingRoom.roomPosY].isUsed) // �ִ�, �ּڰ� �ѱ��� �ʱ�
                {
                    aroundRooms.Add(dungeonSize[usingRoom.roomPosX + 1, usingRoom.roomPosY]);
                }
                if (usingRoom.roomPosX - 1 >= 0 &&
                    !dungeonSize[usingRoom.roomPosX - 1, usingRoom.roomPosY].isUsed)
                {
                    aroundRooms.Add(dungeonSize[usingRoom.roomPosX - 1, usingRoom.roomPosY]);
                }
                if (usingRoom.roomPosY + 1 < maxYValue &&
                    !dungeonSize[usingRoom.roomPosX, usingRoom.roomPosY + 1].isUsed)
                {
                    aroundRooms.Add(dungeonSize[usingRoom.roomPosX, usingRoom.roomPosY + 1]);
                }
                if (usingRoom.roomPosY - 1 >= 0 &&
                    !dungeonSize[usingRoom.roomPosX, usingRoom.roomPosY - 1].isUsed)
                {
                    aroundRooms.Add(dungeonSize[usingRoom.roomPosX, usingRoom.roomPosY - 1]);
                }
            }

            Room addRoom = aroundRooms[rand.Next(aroundRooms.Count)];
            dungeonList.Add(addRoom);
            addRoom.IsUsedRoom();

            makeDungeonRoomsCount--;
            yield return null;
        }

        //StartCoroutine(SetDisconnectRooms());

        List<Room> forRoomSetList = new List<Room>();

        foreach (var dungeon in dungeonList)
        {
            if (dungeon == currentRoom || CheckOverSpecialRooms()) { continue; }

            if (dungeon.CountedRoomAround() == 1)
            {
                dungeon.roomType = RoomType.boss;
            }
            else if(dungeon.roomType != RoomType.boss || dungeon.roomType != RoomType.start)
            {
                forRoomSetList.Add(dungeon);
            }
        }

        while(CountRoomType(RoomType.spawnMonster) < SpawnMonsterRoomsCount)
        {
            var randomRoom = forRoomSetList[rand.Next(forRoomSetList.Count)];

            randomRoom.roomType = RoomType.spawnMonster;
        }

        while(CountRoomType(RoomType.golden) < goldRoomsCount)
        {
            var randomRoom = forRoomSetList[rand.Next(forRoomSetList.Count)];

            randomRoom.roomType = RoomType.golden;
        }

        if (CheckNullBossRoom())
        {
            dungeonList[UnityEngine.Random.Range(0, dungeonList.Count)].roomType = RoomType.boss;
        }

        for (int i = 0; i < maxXValue; i++)
        {
            for (int j = 0; j < maxYValue; j++)
            {
                if (!dungeonSize[i, j].isUsed)
                {
                    dungeonSize[i, j].gameObject.SetActive(false);
                }
            }
        }

        GameManager.Instance.SetPlayerOnStartRoom();

        onRoomChange?.Invoke();
    }

    private bool CheckOverSpecialRooms()
    {
        int countedGold = 0;
        foreach (var dungeon in dungeonList)
        {
            if (dungeon.roomType == RoomType.boss)
            {
                Debug.Log("������ �ʰ�");
                return true;
            }
            if(dungeon.roomType == RoomType.golden)
            {
                countedGold++;
                if(countedGold > 2)
                {
                    Debug.Log("Ȳ�ݹ� �ʰ�");
                    return true;
                }
            }
        }
        return false;
    }

    private bool CheckNullBossRoom()
    {
        foreach (var dungeon in dungeonList)
        {
            if(dungeon.roomType == RoomType.boss)
            {
                return false;
            }
        }
        return true;
    }

    private int CountRoomType(RoomType type)
    {
        int count = 0;

        foreach (var dungeon in dungeonList)
        {
            if(dungeon.roomType == type)
            {
                count++;
            }
        }
        return count;
    }

    public IEnumerator SetDisconnectRooms()
    {
        int value = disconnectedRoomCount;

        int setInList = 0;

        Debug.Log("�� ���� ���� ����");

        while (value > 0)
        {
            var findDisconnectRoom = dungeonList[rand.Next(0, dungeonList.Count)];

            // ���� ������� �ϴ� ���� ���� ������ �ִ°� üũ
            if (CheckConnectLeast(findDisconnectRoom)) { continue; }

            Debug.Log($"���� �� �̸� :{findDisconnectRoom.name} ");

            float randF = UnityEngine.Random.Range(0f, 1f);

            if (randF >= 0f) // Ȯ���� ���� ���� ����
            {
                if (findDisconnectRoom.leftRoom != null)
                {
                    var leftRoom = findDisconnectRoom.leftRoom;

                    if (CheckConnectLeast(leftRoom)) { continue; }

                    leftRoom.rightDoor.gameObject.SetActive(false);
                    findDisconnectRoom.leftDoor.gameObject.SetActive(false);

                    findDisconnectRoom.leftRoom = null;

                    Debug.Log($"{findDisconnectRoom.name}�� ���� �� ��������");
                }
            }
            else if (randF > 0.25f) // ������
            {
                if (findDisconnectRoom.rightRoom != null)
                {
                    var rightRoom = findDisconnectRoom.rightRoom;

                    if (CheckConnectLeast(rightRoom)) { continue; }

                    rightRoom.leftDoor.gameObject.SetActive(false);
                    findDisconnectRoom.rightDoor.gameObject.SetActive(false);

                    findDisconnectRoom.rightRoom = null;

                    Debug.Log($"{findDisconnectRoom.name}�� ������ �� ��������");
                }
            }
            else if (randF > 0.5f) // ��
            {
                if (findDisconnectRoom.topRoom != null)
                {
                    var topRoom = findDisconnectRoom.topRoom;

                    if (CheckConnectLeast(topRoom)) { continue; }

                    topRoom.bottomDoor.gameObject.SetActive(false);
                    findDisconnectRoom.topDoor.gameObject.SetActive(false);

                    findDisconnectRoom.topRoom = null;

                    Debug.Log($"{findDisconnectRoom.name}�� ���� �� ��������");
                }
            }
            else if (randF > 0.75f) // �Ʒ�
            {
                if (findDisconnectRoom.bottomRoom != null)
                {
                    var bottomRoom = findDisconnectRoom.bottomRoom;

                    if (CheckConnectLeast(bottomRoom)) { continue; }

                    bottomRoom.topDoor.gameObject.SetActive(false);
                    findDisconnectRoom.bottomDoor.gameObject.SetActive(false);

                    findDisconnectRoom.bottomRoom = null;

                    Debug.Log($"{findDisconnectRoom.name}�� �Ʒ��� �� ��������");
                }
            }

            setInList++;

            value--;

            yield return new WaitForSeconds(0.1f);
        }
    }

    //TODO : ���� �̵��� �� �ִ��� üũ �ʿ� (�ƿ� ���� ���� ���� ����)

    private bool CheckConnectLeast(Room targetRoom)
    {
        if(targetRoom.CountedRoomAround() <= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
