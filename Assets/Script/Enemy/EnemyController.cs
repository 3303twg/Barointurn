using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour, IPoolObject, IDamageable
{

    public EnemyStat enemyStat;
    private string originName;


    [Header("레이어 관련")]
    [SerializeField] private LayerMask targetLayer;

    [HideInInspector]
    public Transform target;

    private SpriteRenderer sr;
    private EnemyAnimationController enemyAnimationController;

    public bool isDead = false;
    Collider2D collider2d;

    HpBarHandler hpBarHandler;

    public void Init()
    {
        isDead = false;
        collider2d.enabled = true;
        gameObject.layer = 8;

        //알파 값 초기화
        Color c = sr.color;
        c.a = 1;
        sr.color = c;

        hpBarHandler.Init();
    }
    public void StatConvertor(EnemyStat_So data)
    {
        enemyStat.Name = data.Name;
        enemyStat.MaxHP = data.MaxHP;
        enemyStat.CurHP = enemyStat.MaxHP;
        enemyStat.MinExp = data.MinExp;
        enemyStat.MaxExp = data.MaxExp;
        enemyStat.Attack = data.Attack;
        enemyStat.AttackRange = data.AttackRange;
        enemyStat.AttackSpeed = data.AttackSpeed;
        enemyStat.MoveSpeed = data.MoveSpeed;
        enemyStat.DropItem = data.DropItem;

        Init();
    }

    private void Awake()
    {
        EventBus.Subscribe("GameOver", ReturnPool);
        targetLayer = LayerMask.GetMask("Player");
        sr = gameObject.GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();
        enemyAnimationController = GetComponent<EnemyAnimationController>();
        hpBarHandler = GetComponent<HpBarHandler>();
    }
    void Start()
    {
        target = PlayerController.Instance.transform;
    }

    float distance;
    float attackTime;
    void Update()
    {
        distance = Vector2.Distance(target.position, transform.position);

        if (distance <= enemyStat.AttackRange)
        {
            if (attackTime >= Time.time)
            {
                attackTime = Time.time + enemyStat.AttackSpeed;
                Attack();
            }
        }
    
    }

    private void FixedUpdate()
    {
        if (isDead == false)
        {
            transform.position += (target.position - transform.position).normalized * enemyStat.MoveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead == false)
        {
            if (((1 << collision.gameObject.layer) & targetLayer) != 0)
            {
                collision.GetComponent<IDamageable>().OnDamage(enemyStat.Attack);
            }
        }
    }

    public void OnDamage(float value)
    {
        if (isDead == true) return;

        GameManager.Instance.damageFontManager.ShowDamage(transform.position, value);

        enemyStat.CurHP = Mathf.Max(enemyStat.CurHP - value , 0);
        hpBarHandler.RefrashHp(enemyStat.CurHP / enemyStat.MaxHP);
        if (enemyStat.CurHP <= 0)
        {
            isDead = true;
            collider2d.enabled = false;
            gameObject.layer = 0;
            //애니 연출도 해야하고
            //ReturnPool();

            DropItem();
            enemyAnimationController.Die();
            Die();
        }
        else
        {
            enemyAnimationController.Hit();
        }
    }

    public void InitName(string enemyName) 
    {
        originName = enemyName;
    }


    public void ReturnPool(object obj)
    {
        ObjectPoolManager.Return(gameObject, originName);
    }

    public void ReturnPool()
    {
        ObjectPoolManager.Return(gameObject, originName);
    }



    float dropChance = 95;
    public void DropItem()
    {
        
        if (Random.Range(0f, 100f) <= dropChance) 
        {
            Instantiate(enemyStat.DropItem[Random.Range(0, enemyStat.DropItem.Count)],transform.position, Quaternion.identity);
        }
        

    }

    float dieAniDuration = 2f;
    private void Die()
    {

        float alpha = sr.color.a;
        DoTweenExtensions.TweenFloat(alpha, 0f, dieAniDuration, (alpha) =>
        {
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        },
        () =>
        {
            ReturnPool();
        }
        );
    }

    public void Attack()
    {
        target.GetComponent<IDamageable>().OnDamage(enemyStat.Attack);
    }
}
