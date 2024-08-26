using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplordeEffect : MonoBehaviour
{
    public void OnReturnEffect()
    {
        PoolingManager.Instance.Push(this);
    }
}
