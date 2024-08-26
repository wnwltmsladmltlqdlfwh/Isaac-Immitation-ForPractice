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

        Debug.Log("아이템 스포너 끌어오기");
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
            // 주위 방에 대한 정보를 저장할 List
            List<Room> aroundRooms = new List<Room>();

            // 현재 사용중인 던전 List들의 4방향에 있는 방들의 정보 가져오기
            foreach (Room usingRoom in dungeonList)
            {
                if(usingRoom.roomPosX + 1 < maxXValue &&
                    !dungeonSize[usingRoom.roomPosX + 1, usingRoom.roomPosY].isUsed) // 최대, 최솟값 넘기지 않기
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
                Debug.Log("보스방 초과");
                return true;
            }
            if(dungeon.roomType == RoomType.golden)
            {
                countedGold++;
                if(countedGold > 2)
                {
                    Debug.Log("황금방 초과");
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

        Debug.Log("문 임의 지정 시작");

        while (value > 0)
        {
            var findDisconnectRoom = dungeonList[rand.Next(0, dungeonList.Count)];

            // 현재 지우고자 하는 방이 문을 가지고 있는가 체크
            if (CheckConnectLeast(findDisconnectRoom)) { continue; }

            Debug.Log($"선택 방 이름 :{findDisconnectRoom.name} ");

            float randF = UnityEngine.Random.Range(0f, 1f);

            if (randF >= 0f) // 확률에 따라 왼쪽 제거
            {
                if (findDisconnectRoom.leftRoom != null)
                {
                    var leftRoom = findDisconnectRoom.leftRoom;

                    if (CheckConnectLeast(leftRoom)) { continue; }

                    leftRoom.rightDoor.gameObject.SetActive(false);
                    findDisconnectRoom.leftDoor.gameObject.SetActive(false);

                    findDisconnectRoom.leftRoom = null;

                    Debug.Log($"{findDisconnectRoom.name}의 왼쪽 방 연결해제");
                }
            }
            else if (randF > 0.25f) // 오른쪽
            {
                if (findDisconnectRoom.rightRoom != null)
                {
                    var rightRoom = findDisconnectRoom.rightRoom;

                    if (CheckConnectLeast(rightRoom)) { continue; }

                    rightRoom.leftDoor.gameObject.SetActive(false);
                    findDisconnectRoom.rightDoor.gameObject.SetActive(false);

                    findDisconnectRoom.rightRoom = null;

                    Debug.Log($"{findDisconnectRoom.name}의 오른쪽 방 연결해제");
                }
            }
            else if (randF > 0.5f) // 위
            {
                if (findDisconnectRoom.topRoom != null)
                {
                    var topRoom = findDisconnectRoom.topRoom;

                    if (CheckConnectLeast(topRoom)) { continue; }

                    topRoom.bottomDoor.gameObject.SetActive(false);
                    findDisconnectRoom.topDoor.gameObject.SetActive(false);

                    findDisconnectRoom.topRoom = null;

                    Debug.Log($"{findDisconnectRoom.name}의 윗쪽 방 연결해제");
                }
            }
            else if (randF > 0.75f) // 아래
            {
                if (findDisconnectRoom.bottomRoom != null)
                {
                    var bottomRoom = findDisconnectRoom.bottomRoom;

                    if (CheckConnectLeast(bottomRoom)) { continue; }

                    bottomRoom.topDoor.gameObject.SetActive(false);
                    findDisconnectRoom.bottomDoor.gameObject.SetActive(false);

                    findDisconnectRoom.bottomRoom = null;

                    Debug.Log($"{findDisconnectRoom.name}의 아래쪽 방 연결해제");
                }
            }

            setInList++;

            value--;

            yield return new WaitForSeconds(0.1f);
        }
    }

    //TODO : 방이 이동할 수 있는지 체크 필요 (아예 갈수 없게 문이 막힘)

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
