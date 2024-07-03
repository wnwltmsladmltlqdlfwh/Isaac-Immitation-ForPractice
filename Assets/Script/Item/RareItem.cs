using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareItem : ItemBase
{
    protected override void ItemAnim()
    {
        base.ItemAnim();

    }

    protected override void ItemGain()
    {
        base.ItemGain();
        Debug.Log("아이템 효과 발동");
    }

    public void ItemGet()
    {
        ItemGain();
    }
}
