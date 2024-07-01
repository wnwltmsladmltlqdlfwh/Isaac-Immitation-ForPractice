using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimation
{
    // string으로 실행하는 애니메이션은 일일히 비교하며 애니메이션을 찾기 때문에 느리다
    // 따라서 Hash값을 미리 저장하여 해당 Hash 값으로 애니메이션을 실행함으로 퍼포먼스가 개선된다.

    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string inputXMovementName = "InputX";
    [SerializeField] private string inputYMovementName = "InputY";
    [SerializeField] private string inputXHeadName = "HeadX";
    [SerializeField] private string inputYHeadName = "HeadY";
    [SerializeField] private string inputXBulletName = "BulletX";
    [SerializeField] private string inputYBulletName = "BulletY";
    [SerializeField] private string inputShootName = "isShoot";

    public int idleParameterHash { get; private set; }
    public int walkParameterHash { get; private set; }
    public int inputXParameterHash { get; private set; }
    public int inputYParameterHash { get; private set; }
    public int inputXHeadHash { get; private set; }
    public int inputYHeadHash { get; private set; }
    public int inputXBulletHash { get; private set; }
    public int inputYBulletHash { get; private set; }
    public int inputShootHash { get; private set; }

    public void Initialize()
    {
        idleParameterHash = Animator.StringToHash(idleParameterName);
        walkParameterHash = Animator.StringToHash(walkParameterName);

        inputXParameterHash = Animator.StringToHash(inputXMovementName);
        inputYParameterHash = Animator.StringToHash(inputYMovementName);

        inputXHeadHash = Animator.StringToHash(inputXHeadName);
        inputYHeadHash = Animator.StringToHash(inputYHeadName);

        inputXBulletHash = Animator.StringToHash(inputXBulletName);
        inputYBulletHash = Animator.StringToHash(inputYBulletName);

        inputShootHash = Animator.StringToHash(inputShootName);
    }
}
