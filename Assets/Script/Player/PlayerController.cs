using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimation AnimationData { get; private set; }

    [field: Header("References")]
    [field: SerializeField] public PlayerSO Data { get; private set; }

    public Rigidbody2D rb2D { get; private set; }

    public Animator HeadAnimator;
    public Animator BodyAnimator;

    public GameObject bodyObj;
    public GameObject headObj;
    public GameObject GetItemObj;
    public GameObject DeadPlayerObj;
    public GameObject EndPlayerObj;
    public SpriteRenderer itemShowPos;

    private PlayerStateMachine stateMachine;
    private float passedTime = 0f;

    private float damagedDelay = 0f;

    [SerializeField] private ObjectType objectType;

    private void Awake()
    {
        stateMachine = new PlayerStateMachine(this);
        rb2D = GetComponent<Rigidbody2D>();

        AnimationData.Initialize();
    }

    void Start()
    {
        stateMachine.ChangedState(stateMachine.idleBodyState);

        PlayerManager.Instance.InitPlayerData(this.Data);
        ObjectManager.Instance.GetPlayerInfo(this);
    }

    void Update()
    {
        if (damagedDelay > 0f)
        {
            damagedDelay -= Time.deltaTime;
        }

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
        if (isShooting)
        {
            float savedSpeed = HeadAnimator.speed;

            passedTime += Time.deltaTime;
            if ((1.0f / (PlayerManager.Instance.AttackSpeed)) <= passedTime)
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


        if (PlayerManager.Instance.bulletOptions["ThirdBullet"])
        {
            var sideBullet_L = PoolingManager.Instance.Pop(PlayerManager.Instance.currentBullet);
            var sideBullet_R = PoolingManager.Instance.Pop(PlayerManager.Instance.currentBullet);

            Vector2 leftDir = Quaternion.Euler(0, 0, 45) * InputManager.Instance.bulletDir;
            // 오른쪽 45도 회전 벡터 계산
            Vector2 rightDir = Quaternion.Euler(0, 0, -45) * InputManager.Instance.bulletDir;

            sideBullet_L.GetComponent<BulletBase>()
                .Init(leftDir, headObj.transform.position);
            sideBullet_R.GetComponent<BulletBase>()
                .Init(rightDir, headObj.transform.position);
        }
    }

    public IEnumerator GetItemMotions(Sprite _sprite)
    {
        headObj.SetActive(false);
        bodyObj.SetActive(false);
        GetItemObj.SetActive(true);
        itemShowPos.sprite = _sprite;

        yield return new WaitForSeconds(0.5f);

        headObj.SetActive(true);
        bodyObj.SetActive(true);
        GetItemObj.SetActive(false);
        itemShowPos.sprite = null;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyController>() != null)
        {
            OnDamaged();
        }
    }

    public void OnDamaged()
    {
        if (damagedDelay > 0f) { return; }

        damagedDelay = 3f;
        PlayerManager.Instance.CurHP--;
        _ = StartCoroutine(DamagedEffect());
    }
    private IEnumerator DamagedEffect()
    {
        while (damagedDelay > 0)
        {
            headObj.GetComponent<SpriteRenderer>().color = Color.white;
            bodyObj.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.1f);
            headObj.GetComponent<SpriteRenderer>().color = Color.gray;
            bodyObj.GetComponent<SpriteRenderer>().color = Color.gray;
            yield return new WaitForSeconds(0.1f);
            headObj.GetComponent<SpriteRenderer>().color = Color.white;
            bodyObj.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void PlayerDeadMotion()
    {
        headObj.SetActive(false);
        bodyObj.SetActive(false);
        DeadPlayerObj.SetActive(true);
    }

    public IEnumerator PlayerEndMotion()
    {
        headObj.SetActive(false);
        bodyObj.SetActive(false);
        EndPlayerObj.SetActive(true);

        yield return null;
    }
}