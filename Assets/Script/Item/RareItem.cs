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
        Debug.Log("������ ȿ�� �ߵ�");
    }

    public void ItemGet()
    {
        ItemGain();
    }
}
