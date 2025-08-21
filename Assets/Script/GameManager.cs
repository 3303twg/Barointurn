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
        //���Ⱑ �ٲ�ٰų� Ǯ�� �� �������ϰ� �����Ѵ��ϸ� �ٸ������� ���°� �°ڴµ�
        //�ϴ� �׷��� ������ ����� �ؾ���
        //����Ʈ�� ���⼭ ����������
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
    [Header("���̵� ��� ����")]
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
