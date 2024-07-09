using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimation
{
    // string���� �����ϴ� �ִϸ��̼��� ������ ���ϸ� �ִϸ��̼��� ã�� ������ ������
    // ���� Hash���� �̸� �����Ͽ� �ش� Hash ������ �ִϸ��̼��� ���������� �����ս��� �����ȴ�.

    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string inputXMovementName = "InputX";
    [SerializeField] private string inputYMovementName = "InputY";
    [SerializeField] private string inputXHeadName = "HeadX";
    [SerializeField] private string inputYHeadName = "HeadY";
    [SerializeField] private string inputShootName = "ShootTrigger";

    public int idleParameterHash { get; private set; }
    public int walkParameterHash { get; private set; }
    public int inputXParameterHash { get; private set; }
    public int inputYParameterHash { get; private set; }
    public int inputXHeadHash { get; private set; }
    public int inputYHeadHash { get; private set; }
    public int inputShootHash { get; private set; }

    public void Initialize()
    {
        idleParameterHash = Animator.StringToHash(idleParameterName);
        walkParameterHash = Animator.StringToHash(walkParameterName);

        inputXParameterHash = Animator.StringToHash(inputXMovementName);
        inputYParameterHash = Animator.StringToHash(inputYMovementName);

        inputXHeadHash = Animator.StringToHash(inputXHeadName);
        inputYHeadHash = Animator.StringToHash(inputYHeadName);

        inputShootHash = Animator.StringToHash(inputShootName);
    }
}
