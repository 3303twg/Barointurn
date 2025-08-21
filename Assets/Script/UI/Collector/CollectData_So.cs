using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GameData/CollectData_SO")]
public class CollectData_So : ScriptableObject
{
    public List<EnemyStat_So> enemyData;

    //Áö±ÝÀº ¾È¾¸
    public List<ItemData_So> itemData;
}
