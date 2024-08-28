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


    [Header("문 배경 이미지")]
    [SerializeField] private SpriteRenderer closeDoorObject;
    [SerializeField] private SpriteRenderer openDoorObject;
    [SerializeField] private SpriteRenderer closeDoorLeftSR;
    [SerializeField] private SpriteRenderer closeDoorRightSR;

    [SerializeField] private Sprite closeBossLeftSprite;
    [SerializeField] private Sprite closeBossRightSprite;
    [SerializeField] private Sprite openBossSprite;

    [SerializeField] private Sprite closeOtherLeftSprite;
    [SerializeField] private Sprite closeOtherRightSprite;
    [SerializeField] private Sprite openOtherSprtie;

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
            DungeonManager.Instance.onRoomChange?.Invoke();
        }
    }

    private void SetDoorLocked()
    {
        switch (isOpen)
        {
            case true:
                //doorRenderer.color = Color.white;
                openDoorObject.gameObject.SetActive(true);
                closeDoorObject.gameObject.SetActive(false);
                break;
            case false:
                //doorRenderer.color = Color.red;
                openDoorObject.gameObject.SetActive(false);
                closeDoorObject.gameObject.SetActive(true);
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

        if (nearRoom == RoomType.boss)
        {
            openDoorObject.sprite = openBossSprite;
            closeDoorLeftSR.sprite = closeBossLeftSprite;
            closeDoorRightSR.sprite = closeBossRightSprite;
        }
        else
        {
            openDoorObject.sprite = openOtherSprtie;
            closeDoorLeftSR.sprite = closeOtherLeftSprite;
            closeDoorRightSR.sprite = closeOtherRightSprite;
        }
    }
}
