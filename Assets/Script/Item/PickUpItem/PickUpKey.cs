using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKey : PickUpItem
{
    public override void Init()
    {
        base.Init();
    }

    public override void GainPerformance()
    {

        if (PlayerManager.Instance.KeyItem >= 99) return;

        PlayerManager.Instance.KeyItem += 1;

        base.GainPerformance();
    }
}
