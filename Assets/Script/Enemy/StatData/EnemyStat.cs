using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyStat
{
    public string MonsterID;
    public string Name;
    public string Description;

    public float Attack;
    public float AttackMul;

    public float MaxHP;
    public float MaxHPMul;
    public float CurHP;

    public float AttackRange;
    public float AttackRangeMul;
    public float AttackSpeed;
    public float MoveSpeed;

    public int MinExp;
    public int MaxExp;
    //public string DropItem; // string으로 변경
    //[System.NonSerialized] public List<GameObject> DropItemData = new List<GameObject>();
    public List<GameObject> DropItem = new List<GameObject>();
}


[System.Serializable]
public class EnemyStatDataWrapper
{
    public EnemyStat[] Monster;
}