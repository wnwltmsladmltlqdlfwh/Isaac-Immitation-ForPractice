using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        PlayerManager.Instance.OnHealthChanged += ChangedHealthUI;
        PlayerManager.Instance.OnCurrencyChanged += ChangedCurrencyUI;

        DungeonManager.Instance.onRoomChange += MiniMapUI;
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

    private void MiniMapUI()
    {

    }
}
