using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyTemplate", menuName = "GameData/EnemyTemplate")]
public class EnemyTemplate_So : ScriptableObject
{
    public string MonsterID;   // JSON 키값
    public GameObject prefab;  // 미리 연결된 프리팹
    public Sprite sprite;      // 미리 연결된 스프라이트
}
