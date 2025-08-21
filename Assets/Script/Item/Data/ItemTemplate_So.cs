using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "ItemTemplate", menuName = "GameData/ItemTemplate")]
public class ItemTemplate_So : ScriptableObject
{
    public int ItemID;   // JSON Ű��
    public GameObject prefab;  // �̸� ����� ������
    public Sprite sprite;      // �̸� ����� ��������Ʈ
}
