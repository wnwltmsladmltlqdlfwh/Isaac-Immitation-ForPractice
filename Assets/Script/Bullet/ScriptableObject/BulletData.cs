using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BulletData
{
    [field : Header("ÃÑ¾Ë Á¤º¸")]
    [field: SerializeField][field: Range(0f, 50f)] public float bulletSpeed { get; private set; } = 5f;
    [field: SerializeField][field: Range(0f, 50f)] public float bulletRange { get; private set; } = 5f;
    [field: SerializeField][field: Range(0f, 50f)] public float bulletPower { get; private set; } = 5f;
    [field: SerializeField][field: Range(0f, 50f)] public float bulletSize { get; private set; } = 5f;
}
