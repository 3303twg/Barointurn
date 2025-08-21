using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>, IDamageable
{
    [Header("스탯")]
    public PlayerStat playerStat;

    [Header("탐지 범위")]
    [SerializeField] private float detectRadius = 10f;

    [Header("레이어 관련")]
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private LayerMask targetLayer;

    private Rigidbody2D rb;
    private Vector2 movement;

    public GameObject bullet;
    public Transform target;


    [HideInInspector]
    public HpBarHandler hpBarHandler;


    EnemyAnimationController animationController;



    public void Init()
    {
        animationController.Init();
        animationController.ResetMode();
    }
    public void StatConvertor(PlayerStat_So data)
    {
        playerStat.MaxHp = data.MaxHp;
        playerStat.CurHp = playerStat.MaxHp;
        playerStat.AttackPower = data.AttackPower;
        playerStat.AttackRange = data.AttackRange;
        playerStat.AttackSpeed = data.AttackSpeed;
        playerStat.MoveSpeed = data.MoveSpeed;

        Init();
    }


    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        hpBarHandler = GetComponent<HpBarHandler>();
        animationController = GetComponent<EnemyAnimationController>();
    }


    float attackTime;
    void Update()
    {
        Move();

        if (attackTime <= Time.time)
        {
            target = Detect();
            if (target != null)
            {
                Attack();
                attackTime = Time.time + playerStat.AttackSpeed;
            }
        }



    }


    void FixedUpdate()
    {
        rb.velocity = movement * playerStat.MoveSpeed;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & interactLayer) != 0)
        {
            collision.GetComponent<Iinteractable>().OnInteract();
        }
    }


    public void Move()
    {
        //빠른구현
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;
    }



    Collider2D[] results = new Collider2D[25];
    public Transform Detect()
    {
        //논얼록 사용해서 GC 발생 최소화
        int cnt = Physics2D.OverlapCircleNonAlloc(transform.position, detectRadius, results, targetLayer);

        Collider2D closest = null;
        float minSqrDist = float.MaxValue;

        for (int i = 0; i < cnt; i++)
        {
            //제곱근 연산 생략해서 연산량 줄이기
            float sqrDist = (results[i].transform.position - transform.position).sqrMagnitude;

            if (sqrDist < minSqrDist)
            {
                minSqrDist = sqrDist;
                closest = results[i];
            }
        }

        if (closest != null)
            return closest.transform;

        else
            return null;
    }


    public void Attack()
    {
        GameObject go = ObjectPoolManager.Get(bullet.name);
        var bulletObj = go.GetComponent<BulletController>();

        bulletObj.transform.position = transform.position;
        bulletObj.playerStat = playerStat;
        bulletObj.targetLayer = targetLayer;
        bulletObj.targetPos = target.position;
        bulletObj.direction = (target.transform.position - transform.position).normalized;
    }

    public void OnDamage(float value)
    {
        GameManager.Instance.damageFontManager.ShowDamage(transform.position, value);

        playerStat.CurHp = Mathf.Max(playerStat.CurHp - value, 0);
        hpBarHandler.RefrashHp(playerStat.CurHp / playerStat.MaxHp);
        if (playerStat.CurHp <= 0)
        {
            animationController.UnscaledMode();
            animationController.Die();
            Die();
        }
        else
        {
            animationController.Hit();
        }


    }

    public void Die()
    {
        GameManager.Instance.GameOver();
    }
}
