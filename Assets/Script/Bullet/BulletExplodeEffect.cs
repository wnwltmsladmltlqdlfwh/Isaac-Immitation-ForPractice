using UnityEngine;

public class BulletExplodeEffect : MonoBehaviour
{
    public void OnReturnEffect()
    {
        PoolingManager.Instance.Push(this);
    }
}
