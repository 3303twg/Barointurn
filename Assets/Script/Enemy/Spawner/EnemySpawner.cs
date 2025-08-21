using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Cysharp.Threading.Tasks.Triggers;

public class EnemySpawner : Singleton<EnemySpawner>
{

    [SerializeField]
    private int poolCnt = 10;
    public EnemySpawnData_SO enemySpawnData_So;
    public List<EnemyStat_So> enemyList;

    [HideInInspector]
    public Transform target;

    [Header("스폰반경")][SerializeField]
    private float radius = 10f;

    protected override async void Awake()
    {
        base.Awake();

        enemyList = enemySpawnData_So.EnemySoList;
        //스포너 So순회해서 풀 생성
        foreach(var data in enemySpawnData_So.EnemySoList)
        {
            ObjectPoolManager.CreatePool(data.prefab.name, data.prefab, poolCnt);
            //프레임 분산시키는 용도
            await UniTask.Yield();
        }
    }

    private void Start()
    {
        target = PlayerController.Instance.transform;
    }


    public void SpawnEnemy(float ratio)
    {
        EnemyStat_So data = enemyList[Random.Range(0, enemyList.Count)];
        //스폰 데이터중 랜덤한거 하나 꺼내기 가중치 없음

        GameObject go = ObjectPoolManager.Get(data.prefab.name);
        go.transform.position = GetSpawnPos();
        EnemyController enemy = go.GetComponent<EnemyController>();
        enemy.StatConvertor(data);
        enemy.enemyStat.MaxHP *= ratio;
        enemy.enemyStat.CurHP = enemy.enemyStat.MaxHP;
        //스폰위치잠깐 이상할수도?
    }


    public Vector3 GetSpawnPos()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);

        float x = target.position.x + radius * Mathf.Cos(angle);
        float y = target.position.y + radius * Mathf.Sin(angle);

        return new Vector2(x, y);
    }
}
