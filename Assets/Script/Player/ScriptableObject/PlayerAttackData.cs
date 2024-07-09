using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAttackData
{
    [field: Header("АјАн")]
    [field: SerializeField][field: Range(0f, 50f)] public float attackSpeed { get; private set; } = 1f;
    [field: SerializeField][field: Range(0f, 50f)] public float attackPower { get; private set; } = 1f;
    [field: SerializeField][field: Range(0f, 50f)] public float attackRange { get; private set; } = 1f;
    [field: SerializeField][field: Range(0f, 50f)] public float bulletSpeed { get; private set; } = 1f;
}
