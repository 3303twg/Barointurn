using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyStat", menuName = "GameData/EnemyStat")]
public class EnemyStat_So : ScriptableObject
{
    public string MonsterID;
    public EnemyTemplate_So template;

    public GameObject prefab;
    public Sprite sprite;

    public string Name;
    public string Description;
    public float Attack;
    public float AttackMul;
    public float MaxHP;
    public float MaxHPMul;
    public float AttackRange;
    public float AttackRangeMul;
    public float AttackSpeed;
    public float MoveSpeed;
    public int MinExp;
    public int MaxExp;
    public List<GameObject> DropItem = new List<GameObject>();
}
