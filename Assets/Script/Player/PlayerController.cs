using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [field : Header("Animations")]
    [field : SerializeField] public PlayerAnimation AnimationData { get; private set; }

    [field : Header("References")]
    [field : SerializeField] public PlayerSO Data { get; private set; }

    public Rigidbody2D rb2D { get; private set; }

    public Animator HeadAnimator;
    public Animator BodyAnimator;

    public GameObject bodyObj;
    public GameObject headObj;
    public GameObject GetItemObj;
    public SpriteRenderer itemShowPos;

    private PlayerStateMachine stateMachine;
    private float passedTime = 0f;

    private ObjectType objectType;

    private void Awake()
    {
        stateMachine = new PlayerStateMachine(this);
        rb2D = GetComponent<Rigidbody2D>();

        PlayerManager.Instance.InitPlayerData(this.Data);
        ObjectManager.Instance.GetPlayerInfo(this);

        AnimationData.Initialize();
    }

    void Start()
    {
        stateMachine.ChangedState(stateMachine.idleBodyState);
    }

    void Update()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {

    }

    public void MoveCharactor(Vector2 moveDir)
    {
        rb2D.AddForce(moveDir * PlayerManager.Instance.MoveSpeed);

        BodyAnimator.SetFloat(AnimationData.inputXParameterHash, moveDir.x);
        BodyAnimator.SetFloat(AnimationData.inputYParameterHash, moveDir.y);
    }

    public void StopMovement()
    {
        // 현재 속도
        Vector2 curVelocity = rb2D.velocity;

        //감속
        Vector2 newVelocity = Vector2.Lerp(curVelocity, Vector2.zero, PlayerManager.Instance.Deceleration * Time.deltaTime);

        rb2D.velocity = newVelocity;
    }

    public void HeadDirection(bool isShooting)
    {
        passedTime += Time.deltaTime;

        if (isShooting)
        {
            float savedSpeed = HeadAnimator.speed;

            if(passedTime > PlayerManager.Instance.AttackSpeed)
            {
                passedTime = 0f;

                HeadAnimator.SetTrigger(AnimationData.inputShootHash);

                // 발사
                OnShootPoint();
            }

            HeadAnimator.SetFloat(AnimationData.inputXHeadHash, InputManager.Instance.bulletDir.x);
            HeadAnimator.SetFloat(AnimationData.inputYHeadHash, InputManager.Instance.bulletDir.y);
        }
        else
        {
            HeadAnimator.SetFloat(AnimationData.inputXHeadHash, InputManager.Instance.moveDir.x);
            HeadAnimator.SetFloat(AnimationData.inputYHeadHash, InputManager.Instance.moveDir.y);
        }
    }

    public void OnShootPoint()
    {
        var bulletGo = PoolingManager.Instance.Pop(PlayerManager.Instance.currentBullet);

        bulletGo.GetComponent<BulletBase>()
            .Init(InputManager.Instance.bulletDir, headObj.transform.position);
    }

    private IEnumerator GetItemMotions(Sprite _sprite)
    {
        Debug.Log("아이템 획득 애니메이션 실행");

        headObj.SetActive(false);
        bodyObj.SetActive(false);
        GetItemObj.SetActive(true);
        itemShowPos.sprite = _sprite;

        yield return new WaitForSeconds(0.5f);

        headObj.SetActive(true);
        bodyObj.SetActive(true);
        GetItemObj.SetActive(false);
        itemShowPos.sprite = null;

        Debug.Log("아이템 획득 애니메이션 종료");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject getItem = collision.gameObject;

        if (getItem.layer.ToString() != "Item")
        {
            getItem.GetComponent<ItemBase>().AddStat();

            if (getItem.CompareTag("UseAnimItem"))
            {
                _ = StartCoroutine("GetItemMotions",
                    collision.gameObject.GetComponent<ItemBase>().ItemData.ItemIconSprite);
            }

            if(getItem.GetComponent<RareItem>() != null)
            {
                getItem.GetComponent<RareItem>().ItemGet();
            }

            Destroy(getItem);
        }
    }
}
