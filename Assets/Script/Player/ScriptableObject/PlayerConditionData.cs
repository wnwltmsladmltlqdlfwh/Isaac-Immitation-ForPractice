using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerConditionData
{
    [field: Header("Ã¼·Â")]
    [field: SerializeField][field: Range(0, 50)] public int StartHealthPoint { get; private set; } = 3;
    [field: SerializeField][field: Range(0, 50)] public int StartShiledPoint { get; private set; } = 0;
}
