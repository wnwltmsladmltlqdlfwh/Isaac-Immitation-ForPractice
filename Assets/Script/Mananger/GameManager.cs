using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public SpiderController testMonster;

    private int curRoomMonsterCount;

    public int CurRoomMonsterCount
    {
        get { return curRoomMonsterCount; }
        set
        {
            if (curRoomMonsterCount != value)
            {
                curRoomMonsterCount = value;
                DungeonManager.Instance.onRoomChange?.Invoke();

                if(curRoomMonsterCount == 0)
                {
                    ObjectManager.Instance.PoolingPickUpItems();
                }
            }
        }
    }

    Camera mainCam;

    public float spawnCountTime = 1f;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (spawnCountTime >= 0f)
        {
            spawnCountTime -= Time.deltaTime;
        }
    }

    public void SetPlayerOnStartRoom()
    {
        ObjectManager.Instance.Player.transform.position = DungeonManager.Instance.currentRoom.transform.position;
    }

    public void PlayerMoveNextRoom(Door nearDoor)
    {
        switch (nearDoor.dir)
        {
            case DoorDir.left:
                var LRoom = DungeonManager.Instance.currentRoom.leftRoom;
                ObjectManager.Instance.Player.transform.position =
                    new Vector3(LRoom.rightDoor.transform.position.x - 1.5f, LRoom.rightDoor.transform.position.y);
                DungeonManager.Instance.currentRoom = LRoom;
                _ = StartCoroutine(CameraMoveNextRoom(LRoom));
                break;
            case DoorDir.right:
                var RRoom = DungeonManager.Instance.currentRoom.rightRoom;
                ObjectManager.Instance.Player.transform.position =
                    new Vector3(RRoom.leftDoor.transform.position.x + 1.5f, RRoom.leftDoor.transform.position.y);
                DungeonManager.Instance.currentRoom = RRoom;
                _ = StartCoroutine(CameraMoveNextRoom(RRoom));
                break;
            case DoorDir.up:
                var URoom = DungeonManager.Instance.currentRoom.topRoom;
                ObjectManager.Instance.Player.transform.position =
                    new Vector3(URoom.bottomDoor.transform.position.x, URoom.bottomDoor.transform.position.y + 1.5f);
                DungeonManager.Instance.currentRoom = URoom;
                _ = StartCoroutine(CameraMoveNextRoom(URoom));
                break;
            case DoorDir.down:
                var DRoom = DungeonManager.Instance.currentRoom.bottomRoom;
                ObjectManager.Instance.Player.transform.position =
                    new Vector3(DRoom.topDoor.transform.position.x, DRoom.topDoor.transform.position.y - 2.5f);
                DungeonManager.Instance.currentRoom = DRoom;
                _ = StartCoroutine(CameraMoveNextRoom(DRoom));
                break;
        }

        ObjectManager.Instance.Player.rb2D.velocity = Vector3.zero;
    }

    IEnumerator CameraMoveNextRoom(Room nextRoom)
    {
        Vector3 startPos = mainCam.transform.position;
        Vector3 targetPos = new Vector3(nextRoom.transform.position.x, nextRoom.transform.position.y, -20);
        float moveTime = 0.5f;
        float startTime = 0f;

        while (startTime < moveTime)
        {
            startTime += Time.deltaTime;
            float fractionOfJourney = startTime / moveTime;

            mainCam.transform.position = Vector3.Lerp(startPos, targetPos, fractionOfJourney);

            yield return null;
        }

        mainCam.transform.position = targetPos;
    }

    public IEnumerator MonsterSpawn(int a)
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < a; i++)
        {
            Vector3 randPos = (Vector3)UnityEngine.Random.insideUnitCircle * 3f;

            var testmonster = ObjectManager.Instance.Spawn<SpiderController>(testMonster, DungeonManager.Instance.currentRoom.transform.position + randPos);
            testmonster.isDead = false;
        }

        //CurRoomMonsterCount = a;
    }

    public void ItemSpawn()
    {

    }
}
