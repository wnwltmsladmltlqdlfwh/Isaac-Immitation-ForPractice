using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.UIElements;

public class Room : MonoBehaviour
{
    public enum ThisRoomIs
    {
        start,
        nomal,
        boss,
        golden,
        needkey,
    }

    public ThisRoomIs property;
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

    SpriteRenderer SpriteRenderer;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        DungeonManager.Instance.onRoomChange += CheckAroundRoom;
    }

    public void InitRoomArrayPos(int posX, int posY)
    {
        this.roomPosX = posX;
        this.roomPosY = posY;

        this.gameObject.transform.position = new Vector2(roomPosX * 20f, roomPosY * 12f);
    }

    public void IsStartRoom()
    {
        this.isUsed = true;
        this.SpriteRenderer.color = Color.red;
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
        leftDoor.gameObject.SetActive(leftRoom != null);
        rightDoor.gameObject.SetActive(rightRoom != null);
        topDoor.gameObject.SetActive(topRoom != null);
        bottomDoor.gameObject.SetActive(bottomRoom != null);
    }

    private void OnDisable()
    {
        //DungeonManager.Instance.onRoomChange -= CheckAroundRoom;
    }
}
