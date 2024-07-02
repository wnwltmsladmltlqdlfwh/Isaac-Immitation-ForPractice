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

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
