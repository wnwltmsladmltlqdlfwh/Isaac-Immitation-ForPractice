using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerMovementData
{
    [field: Header("¿Ãµø")]
    [field: SerializeField][field: Range(0f, 50f)] public float moveSpeed { get; private set; } = 5f;
    [field: SerializeField][field: Range(0f, 50f)] public float acceleration { get; private set; } = 10f;
    [field: SerializeField][field: Range(0f, 50f)] public float deceleration { get; private set; } = 5f;
}
