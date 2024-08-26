using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ItemSO", menuName = "Item/AddItemBase")]
public class ItemBase : ScriptableObject
{
    [field: Header("References")] 
    [field : SerializeField] public ItemDataSO ItemData;
    
    public string effectKey;
    public bool effectBoolValue;

    public string skinKey;

    public Sprite itemIcon;

    // æ∆¿Ã≈€ »πµÊ ∏ﬁºº¡ˆ
    public string AcquisitionMessage;

    public bool UsedAnimation = false;

    private void SetVisibleOption()
    {
        itemIcon = ItemData.ItemIconSprite;
        UsedAnimation = ItemData.UsedAnimation;
    }

    public void ItemGain()
    {
        PlayerManager.Instance.MaxHP += this.ItemData.MaxHP;
        PlayerManager.Instance.CurHP += this.ItemData.CurHP;
        PlayerManager.Instance.Shiled += this.ItemData.Shiled;

        PlayerManager.Instance.AttackSpeed += this.ItemData.AttackSpeed;
        PlayerManager.Instance.AttackPower += this.ItemData.AttackPower;
        PlayerManager.Instance.AttackRange += this.ItemData.AttackRange;
        PlayerManager.Instance.BulletSpeed += this.ItemData.BulletSpeed;

        PlayerManager.Instance.MoveSpeed += this.ItemData.MoveSpeed;

        PlayerManager.Instance.BombItem += this.ItemData.BoomItem;
        PlayerManager.Instance.MoneyItem += this.ItemData.MoneyItem;
        PlayerManager.Instance.KeyItem += this.ItemData.KeyItem;

        OptionsSet();
        SkinSet();
    }

    private void OptionsSet()
    {
        if (effectKey == null || !PlayerManager.Instance.bulletOptions.ContainsKey(effectKey))
            return;
        else
            PlayerManager.Instance.bulletOptions[effectKey] = effectBoolValue;
    }

    private void SkinSet()
    {
        if(skinKey == null)
            return;

        PlayerManager.Instance.ChangedPlayerSkin(skinKey);
    }
}
