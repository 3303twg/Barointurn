using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GameData/ItemStat_SO")]
public class ItemData_So : ScriptableObject
{
    public int ItemID;
    public ItemTemplate_So template;

    public GameObject prefab;
    public Sprite sprite;

    public string Name;
    public string Description;
    public int UnlockLev;

    public int MaxHP;
    public float MaxHPMul;
    public int MaxMP;
    public float MaxMPMul;
    public int MaxAtk;
    public float MaxAtkMul;
    public int MaxDef;
    public float MaxDefMul;
}
