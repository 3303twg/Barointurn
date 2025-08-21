using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour, IPoolObject
{
    public LayerMask targetLayer;
    public PlayerStat playerStat;

    public Transform target;
    public Vector3 targetPos;

    public float MoveSpeed = 6f;

    public float lifeTime = 4f;

    public float rotateDuration = 0.5f;
    public Vector3 direction;


    private void Awake()
    {
        EventBus.Subscribe("GameOver", ReturnPool);
    }


    private void OnEnable()
    {
        //direction = (targetPos - PlayerController.Instance.transform.position).normalized;
        Invoke(nameof(ReturnPool), lifeTime);

        //¹Ô¹ÔÇÏ´Ï±î È¸Àü
        transform.DORotate(new Vector3(0, 0, -360), rotateDuration, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Restart);
    }



    private void FixedUpdate()
    {
        transform.position += direction * MoveSpeed * Time.deltaTime;
    }







    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            collision.GetComponent<IDamageable>().OnDamage(playerStat.AttackPower);

            ReturnPool();
        }
    }


    string originName;
    public void InitName(string name)
    {
        originName = name;
    }

    public void ReturnPool()
    {
        ObjectPoolManager.Return(gameObject, originName);
    }

    public void ReturnPool(object obj)
    {
        ObjectPoolManager.Return(gameObject, originName);
    }
}
