using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Sprite soulHeart;
    [SerializeField] private Sprite soulHalfHeart;

    [SerializeField] private Image[] playerHealths;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI boomText;
    public TextMeshProUGUI keyText;

    [SerializeField] private Image minimapParent;
    [SerializeField] private Image minimapPrefab;

    [SerializeField] private Image[,] minimapSize;

    public Slider bossHealthUI;
    private float bossHealth;

    private void Awake()
    {
        minimapSize = new Image[DungeonManager.Instance.maxXValue, DungeonManager.Instance.maxYValue];

        PlayerManager.Instance.OnHealthChanged += ChangedHealthUI;
        PlayerManager.Instance.OnCurrencyChanged += ChangedCurrencyUI;

        DungeonManager.Instance.onRoomChange += UpdateMiniMapState;



        InitMiniMap();
    }

    private void Start()
    {
        ChangedCurrencyUI();
    }

    private void ChangedHealthUI()
    {
        foreach (var hp in playerHealths)
        {
            hp.sprite = null;
            hp.gameObject.SetActive(true);
        }

        int fullHeartCount = (PlayerManager.Instance.CurHP - (PlayerManager.Instance.CurHP % 2)) / 2;

        for (int i = 0; i < PlayerManager.Instance.MaxHP / 2; i++)
        {
            if(i < fullHeartCount)
            {
                playerHealths[i].sprite = fullHeart;
            }
            else if((PlayerManager.Instance.CurHP % 2) > 0 && i == fullHeartCount)
            {
                playerHealths[i].sprite = halfHeart;
            }
            else
            {
                playerHealths[i].sprite = emptyHeart;
            }
        }

        foreach (var hp in playerHealths)
        {
            if(hp.sprite == null)
            {
                hp.gameObject.SetActive(false);
            }
        }
    }

    private void ChangedCurrencyUI()
    {
        coinText.text = PlayerManager.Instance.MoneyItem.ToString();
        boomText.text = PlayerManager.Instance.BombItem.ToString();
        keyText.text = PlayerManager.Instance.KeyItem.ToString();
    }

    private void InitMiniMap()
    {
        for (int i = 0; i < DungeonManager.Instance.maxXValue; i++)
        {
            for (int j = 0; j < DungeonManager.Instance.maxYValue; j++)
            {
                var minimapRoom = PoolingManager.Instance.Pop(minimapPrefab);
                minimapRoom.rectTransform.SetParent(minimapParent.rectTransform, false);
                minimapSize[i, j] = minimapRoom;
                minimapRoom.name = $"MiniMap Num {i},{j}";
            }
        }
    }

    private void UpdateMiniMapState()
    {
        for(int i = 0; i < DungeonManager.Instance.maxXValue; i++)
        {
            for(int j = 0; j < DungeonManager.Instance.maxYValue; j++)
            {
                var targetRoom = DungeonManager.Instance.dungeonSize[i, j];

                var setColor = minimapSize[i, j].color;

                if (!targetRoom.isUsed)
                {
                    setColor.a = 0;
                    minimapSize[i,j].color = setColor;
                }
                else
                {
                    if(targetRoom == DungeonManager.Instance.currentRoom)
                    {
                        Debug.Log(targetRoom.name + "은 현재 방입니다.");
                        minimapSize[i, j].color = Color.blue;
                    }
                    else
                    {
                        Debug.Log(targetRoom.name + "세팅");

                        if (targetRoom.isVisited)
                        {
                            minimapSize[i, j].color = Color.white;
                        }
                        else
                        {
                            minimapSize[i, j].color = new Color(0.7f, 0.7f, 0.7f);
                        }
                    }
                }

                if (targetRoom.roomType == RoomType.boss)
                {
                    minimapSize[i, j].color = Color.red;
                }
                else if (targetRoom.roomType == RoomType.golden)
                {
                    minimapSize[i,j].color = Color.yellow;
                }
            }
        }
    }

    public void BossBattleUI(bool isOn)
    {
        bossHealthUI.gameObject.SetActive(isOn);
    }

    public void BossUIUpdate(float curHealth, float maxHealth)
    {
        bossHealthUI.value = curHealth / maxHealth;
    }
}
