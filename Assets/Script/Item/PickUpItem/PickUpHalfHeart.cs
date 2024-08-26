using UnityEngine;

public class PickUpHalfHeart : PickUpItem
{
    public override void Init()
    {
        base.Init();

    }

    public override void GainPerformance()
    {

        if (PlayerManager.Instance.CurHP >= PlayerManager.Instance.MaxHP) return;

        PlayerManager.Instance.CurHP += 1;

        base.GainPerformance();
    }
}
