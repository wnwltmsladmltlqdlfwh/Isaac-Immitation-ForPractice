using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBomb : PickUpItem
{
    public override void Init()
    {
        base.Init();
    }

    public override void GainPerformance()
    {
        if (PlayerManager.Instance.BombItem >= 99) return;

        PlayerManager.Instance.BombItem += 1;

        base.GainPerformance();
    }
}
