using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DungeonManager : Singleton<DungeonManager>
{
    public Room[,] dungeonSize;
    List<Room> dungeonList = new List<Room>();

    [SerializeField] private int maxXValue;
    [SerializeField] private int maxYValue;
    [SerializeField] private Room RoomPrefab;
    [SerializeField] private int disconnectedRoomCount;

    public int makeDungeonRoomsCount;

    public Action onRoomChange;

    Transform roomsParent;

    System.Random rand = new System.Random();

    private void Awake()
    {
        dungeonSize = new Room[maxXValue, maxYValue];

        if (roomsParent == null)
        {
            var parent = Instantiate(new GameObject());
            parent.name = "RoomParent";
            parent.transform.position = Vector3.zero;
            roomsParent = parent.transform;
        }
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
                var roomPrefab = Instantiate(RoomPrefab, roomsParent);
                roomPrefab.name = $"room({i}, {j})";
                dungeonSize[i, j] = roomPrefab;
                roomPrefab.InitRoomArrayPos(i, j);

                if (i == (maxXValue / 2) & j == (maxYValue / 2))
                {
                    dungeonSize[i, j].IsStartRoom();
                    dungeonList.Add(dungeonSize[i, j]);
                }
            }
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

            Debug.Log($"����ϴ� ���� �̸� : {addRoom.name}");

            onRoomChange?.Invoke();
            makeDungeonRoomsCount--;
            Debug.Log(makeDungeonRoomsCount.ToString());
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("�� ���� �Ϸ�");

        StartCoroutine(SetDisconnectRooms());
    }

    public IEnumerator SetDisconnectRooms()
    {
        int value = disconnectedRoomCount;

        int setInList = 0;

        Debug.Log("�� ���� ���� ����");

        while (value > 0)
        {
            var findDisconnectRoom = dungeonList[rand.Next(0, dungeonList.Count)];

            if (findDisconnectRoom.leftRoom == null && findDisconnectRoom.rightRoom == null
                && findDisconnectRoom.topRoom == null && findDisconnectRoom.bottomRoom == null) { continue; }

            Debug.Log($"���� �� �̸� :{findDisconnectRoom.name} ");

            float randF = UnityEngine.Random.Range(0f, 1f);

            if (randF >= 0f) // Ȯ���� ���� ���� ����
            {
                if (findDisconnectRoom.leftRoom != null)
                {
                    var leftRoom = findDisconnectRoom.leftRoom;

                    leftRoom.rightDoor.gameObject.SetActive(false);
                    findDisconnectRoom.leftDoor.gameObject.SetActive(false);
                }
            }
            else if (randF > 0.25f) // ������
            {
                if (findDisconnectRoom.rightRoom != null)
                {
                    var rightRoom = findDisconnectRoom.rightRoom;

                    rightRoom.leftDoor.gameObject.SetActive(false);
                    findDisconnectRoom.rightDoor.gameObject.SetActive(false);
                }
            }
            else if (randF > 0.5f) // ��
            {
                if (findDisconnectRoom.topRoom != null)
                {
                    var topRoom = findDisconnectRoom.topRoom;

                    topRoom.bottomDoor.gameObject.SetActive(false);
                    findDisconnectRoom.topDoor.gameObject.SetActive(false);
                }
            }
            else if (randF > 0.75f) // �Ʒ�
            {
                if (findDisconnectRoom.bottomRoom != null)
                {
                    var bottomRoom = findDisconnectRoom.bottomRoom;

                    bottomRoom.topDoor.gameObject.SetActive(false);
                    findDisconnectRoom.bottomDoor.gameObject.SetActive(false);
                }
            }

            setInList++;

            value--;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
