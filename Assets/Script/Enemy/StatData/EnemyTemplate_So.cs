using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyTemplate", menuName = "GameData/EnemyTemplate")]
public class EnemyTemplate_So : ScriptableObject
{
    public string MonsterID;   // JSON Ű��
    public GameObject prefab;  // �̸� ����� ������
    public Sprite sprite;      // �̸� ����� ��������Ʈ
}
