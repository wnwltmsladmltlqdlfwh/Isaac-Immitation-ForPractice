using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerMovementData
{
    [field: Header("¿Ãµø")]
    [field: SerializeField][field: Range(0f, 50f)] public float moveSpeed { get; private set; } = 5f;


}
