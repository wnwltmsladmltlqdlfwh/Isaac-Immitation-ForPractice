using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFullHeart : PickUpItem
{
    public override void Init()
    {
        base.Init();
    }

    public override void GainPerformance()
    {
        if (PlayerManager.Instance.CurHP >= PlayerManager.Instance.MaxHP) return;

        PlayerManager.Instance.CurHP += 2;

        base.GainPerformance();
    }
}
