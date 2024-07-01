using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour
{
    public void OnShootPoint(int point)
    {
        var bulletGo = PoolingManager.Instance.Pool.Get();

        bulletGo.transform.position = this.transform.position;
    }
}
