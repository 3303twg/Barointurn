using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public float startSpawnTime = 1f;
    public float repeatTime = 1f;

    private int poolCnt = 10;

    public DamageFontManager damageFontManager;

    public PlayerStat_So playerStat_So;

    [HideInInspector]
    public InputController inputController;
    protected override void Awake()
    {
        base.Awake();

        inputController = GetComponent<InputController>();
        damageFontManager = GetComponent<DamageFontManager>();
        //무기가 바뀐다거나 풀을 좀 디테일하게 관리한다하면 다른곳으로 가는게 맞겠는데
        //일단 그런거 없으니 여기다 해야지
        //이펙트도 여기서 하지않을까
        ObjectPoolManager.CreatePool(PlayerController.Instance.bullet.name, PlayerController.Instance.bullet, poolCnt);

        SceneManager.sceneLoaded += Init;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= Init;
    }

    private void Init(Scene arg0, LoadSceneMode arg1)
    {
        ratio = ratioOffSet;
        cnt = 1;
        PlayerController.Instance.StatConvertor(playerStat_So);
        PlayerController.Instance.hpBarHandler.RefrashHp(PlayerController.Instance.playerStat.CurHp / PlayerController.Instance.playerStat.MaxHp);
        UIManager.Instance.GetUI();
        inputController.OffPause();

    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), startSpawnTime, repeatTime);
    }



    float ratioOffSet = 1f;
    public float ratio = 1f;
    [Header("난이도 상승 배율")]
    public float difficultyMultiplier = 1.15f;
    int cnt = 1;
    public void SpawnEnemy()
    {
        cnt++;

        int spawnTimes = 1 + (cnt - 1) / 10;

        for (int i = 0; i < spawnTimes; i++)
        {
            EnemySpawner.Instance.SpawnEnemy(ratio);
        }

        if (cnt % 5 == 0)
        {
            ratio *= difficultyMultiplier;
        }
    }


    public void GameOver()
    {
        EventBus.Publish("GameOver", null);
        UIManager.Instance.ShowUI("GameOverUI");
    }

    public void SceneLoad()
    {
        SceneManager.LoadScene(0);
    }
}
