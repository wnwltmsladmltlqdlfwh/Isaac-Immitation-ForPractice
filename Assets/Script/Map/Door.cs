using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorDir
{
    left,
    right,
    up,
    down
}

public class Door : MonoBehaviour
{
    private SpriteRenderer doorRenderer;

    public DoorDir dir;

    [SerializeField] private Sprite defaultDoor;
    [SerializeField] private Sprite bossDoor;
    [SerializeField] private Sprite goldenDoor;
    [SerializeField] private Sprite needKeyDoor;

    public bool isOpen
    {
        get { return GameManager.Instance.CurRoomMonsterCount == 0; }
    }

    private void Awake()
    {
        doorRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        DungeonManager.Instance.onRoomChange += SetDoorLocked;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isOpen) { return; }

        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.PlayerMoveNextRoom(this);
        }
    }

    private void SetDoorLocked()
    {
        switch (isOpen)
        {
            case true:
                doorRenderer.color = Color.white;
                break;
            case false:
                doorRenderer.color = Color.red;
                break;
        }
    }

    public void SetNearbyRoomDoor(RoomType nearRoom)
    {
        switch (nearRoom)
        {
            case RoomType.boss:
                doorRenderer.sprite = bossDoor;
                break;
            case RoomType.golden:
                doorRenderer.sprite = goldenDoor;
                break;
            case RoomType.needkey:
                doorRenderer.sprite = needKeyDoor;
                break;
            default:
                doorRenderer.sprite = defaultDoor;
                break;
        }
    }
}
