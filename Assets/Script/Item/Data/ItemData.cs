using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public int ItemID;
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

    public int Status;
}

[System.Serializable]
public class ItemDataWrapper
{
    public List<ItemData> Item;
}