using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]
public class PlayerSO : ScriptableObject
{
    [field : SerializeField] public PlayerMovementData playerMovementData { get; private set; }
    [field : SerializeField] public PlayerAttackData playerAttackData { get; private set; }
    [field : SerializeField] public PlayerConditionData playerConditionData { get; private set; }
}
