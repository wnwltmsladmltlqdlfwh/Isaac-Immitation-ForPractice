using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ItemData", menuName = "Item/Data")]
public class ItemDataSO : ScriptableObject
{
    [field: SerializeField][field: Range(0, 50)] public int MaxHP;
    [field: SerializeField][field: Range(0, 50)] public int CurHP;
    [field: SerializeField][field: Range(0, 50)] public int Shiled;

    [field: SerializeField][field: Range(0f, 50f)] public float AttackSpeed;
    [field: SerializeField][field: Range(0f, 50f)] public float AttackPower;
    [field: SerializeField][field: Range(0f, 50f)] public float AttackRange;
    [field: SerializeField][field: Range(0f, 50f)] public float BulletSpeed;

    [field: SerializeField][field: Range(0f, 50f)] public float MoveSpeed;
    [field: SerializeField][field: Range(0f, 50f)]  public float Deceleration;

    [field: SerializeField][field: Range(0, 99)] public int BoomItem;
    [field: SerializeField][field: Range(0, 99)] public int MoneyItem;
    [field: SerializeField][field: Range(0, 99)] public int KeyItem;

    //[field: SerializeField] public Sprite ItemIconSprite;

    //[field: SerializeField] public bool UsedAnimation;
    //[field: SerializeField] public string AcquisitionMessage;
}
