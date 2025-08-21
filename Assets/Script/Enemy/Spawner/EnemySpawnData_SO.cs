using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "SpawnData", menuName = "GameData/SpawnData")]


public class EnemySpawnData_SO : ScriptableObject
{
    public List<EnemyStat_So> EnemySoList;
}
