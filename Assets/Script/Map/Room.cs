using System;
using UnityEngine;

public enum RoomType
{
    start,
    nomal,
    boss,
    golden,
    needkey,
    spawnMonster,
    shop,
}

public class Room : MonoBehaviour
{
    public RoomType roomType;
    public int roomPosX;
    public int roomPosY;

    public Room leftRoom;
    public Room rightRoom;
    public Room topRoom;
    public Room bottomRoom;

    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;


    public bool isUsed = false;
    public bool aroundRoomsFull = false;
    private bool isSpawned = false;
    public bool isVisited = false;

    SpriteRenderer SpriteRenderer;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        DungeonManager.Instance.onRoomChange += CheckAroundRoom;
    }

    private void Start()
    {
    }

    public void InitRoomArrayPos(int posX, int posY)
    {
        this.roomPosX = posX;
        this.roomPosY = posY;

        this.gameObject.transform.position = new Vector2(roomPosX * 40f, roomPosY * 20f);
    }

    public void IsStartRoom()
    {
        this.isUsed = true;
        this.roomType = RoomType.start;
        this.SpriteRenderer.color = Color.red;
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -20);
    }

    public void IsUsedRoom()
    {
        this.isUsed = true;
        this.SpriteRenderer.color = Color.blue;
    }

    private void CheckAroundRoom()
    {
        if (this.aroundRoomsFull == true) { return; }
        
        if (this.aroundRoomsFull == false && this.isUsed == true)
        {
            var dungeonInfo = DungeonManager.Instance.dungeonSize;

            if (roomPosX + 1 < dungeonInfo.GetLength(0) &&
                dungeonInfo[this.roomPosX + 1, this.roomPosY].isUsed == true)
            {
                rightRoom = dungeonInfo[this.roomPosX + 1, this.roomPosY];
            }

            if (roomPosX - 1 >= 0 &&
                dungeonInfo[this.roomPosX - 1, this.roomPosY].isUsed == true)
            {
                leftRoom = dungeonInfo[this.roomPosX - 1, this.roomPosY];
            }

            if (roomPosY + 1 < dungeonInfo.GetLength(1) &&
                dungeonInfo[this.roomPosX, this.roomPosY + 1].isUsed == true)
            {
                topRoom = dungeonInfo[this.roomPosX, this.roomPosY + 1];
            }

            if (roomPosY - 1 >= 0 &&
                dungeonInfo[this.roomPosX, this.roomPosY - 1].isUsed == true)
            {
                bottomRoom = dungeonInfo[this.roomPosX, this.roomPosY - 1];
            }

            if (leftRoom != null && rightRoom != null && topRoom != null && bottomRoom != null)
            {
                aroundRoomsFull = true;
            }
        }

        CheckDoorEnable();
    }

    private void CheckDoorEnable()
    {
        if(leftRoom != null)
        {
            leftDoor.gameObject.SetActive(leftRoom != null);
            leftDoor.dir = DoorDir.left;
            leftDoor.SetNearbyRoomDoor(leftRoom.roomType);
        }
        else
        {
            leftDoor.gameObject.SetActive(false);
        }

        if (rightRoom != null)
        {
            rightDoor.gameObject.SetActive(rightRoom != null);
            rightDoor.dir = DoorDir.right;
            rightDoor.SetNearbyRoomDoor(rightRoom.roomType);
        }
        else
        {
            rightDoor.gameObject.SetActive(false);
        }

        if (topRoom != null)
        {
            topDoor.gameObject.SetActive(topRoom != null);
            topDoor.dir = DoorDir.up;
            topDoor.SetNearbyRoomDoor(topRoom.roomType);
        }
        else
        {
            topDoor.gameObject.SetActive(false);
        }

        if (bottomRoom != null)
        {
            bottomDoor.gameObject.SetActive(bottomRoom != null);
            bottomDoor.dir = DoorDir.down;
            bottomDoor.SetNearbyRoomDoor(bottomRoom.roomType);
        }
        else
        {
            bottomDoor.gameObject.SetActive(false);
        }

        if (this.roomType == RoomType.golden || this.roomType == RoomType.boss)
        {
            leftDoor.SetNearbyRoomDoor(this.roomType);
            rightDoor.SetNearbyRoomDoor(this.roomType);
            topDoor.SetNearbyRoomDoor(this.roomType);
            bottomDoor.SetNearbyRoomDoor(this.roomType);
        }
    }

    public int CountedRoomAround()
    {
        int value = 0;

        if (leftRoom != null)
            value += 1;

        if (rightRoom != null)
            value += 1;

        if (topRoom != null)
            value += 1;

        if (bottomRoom != null)
            value += 1;

        return value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isVisited = true;

            switch (roomType)
            {
                case RoomType.boss:
                    Debug.Log("보스방 입장");
                    if (!isSpawned)
                    {
                        _= StartCoroutine(GameManager.Instance.BossSpawn());
                        isSpawned = !isSpawned;
                    }
                    break;
                case RoomType.spawnMonster:
                    if (!isSpawned)
                    {
                        int randomSpawn = UnityEngine.Random.Range(4,8);
                        GameManager.Instance.CurRoomMonsterCount = randomSpawn;
                        isSpawned = !isSpawned;
                        DungeonManager.Instance.onRoomChange?.Invoke();

                        _ = StartCoroutine(GameManager.Instance.MonsterSpawn(randomSpawn));
                        
                    }
                    break;
                case RoomType.golden:
                    break;
                case RoomType.needkey:
                    break;
                case RoomType.nomal:
                    break;
            }
        }
    }
}
