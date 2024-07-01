using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Bullet", menuName = "Characters/Bullet")]
public class BulletSO : ScriptableObject
{
    [field: SerializeField] public BulletData bulletData { get; private set; }
}
