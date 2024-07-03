using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    [field: Header("References")] 
    [field : SerializeField] public ItemSO ItemData;

    SpriteRenderer itemIcon;

    private void Awake()
    {
        itemIcon = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        itemIcon.sprite = ItemData.ItemIconSprite;
    }

    // æ∆¿Ã≈€ »πµÊ ∏ﬁºº¡ˆ
    public string AcquisitionMessage;

    public void AddStat()
    {
        PlayerManager.Instance.MaxHP += this.ItemData.MaxHP;
        PlayerManager.Instance.CurHP += this.ItemData.CurHP;
        PlayerManager.Instance.Shiled += this.ItemData.Shiled;

        PlayerManager.Instance.AttackSpeed += this.ItemData.AttackSpeed;
        PlayerManager.Instance.AttackPower += this.ItemData.AttackPower;
        PlayerManager.Instance.AttackRange += this.ItemData.AttackRange;
        PlayerManager.Instance.BulletSpeed += this.ItemData.BulletSpeed;

        PlayerManager.Instance.MoveSpeed += this.ItemData.MoveSpeed;

        PlayerManager.Instance.BoomItem += this.ItemData.BoomItem;
        PlayerManager.Instance.MoneyItem += this.ItemData.MoneyItem;
        PlayerManager.Instance.KeyItem += this.ItemData.KeyItem;
    }

    protected virtual void ItemAnim() { }

    protected virtual void ItemGain() { }
}
