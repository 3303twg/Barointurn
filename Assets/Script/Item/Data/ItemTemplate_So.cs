using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "ItemTemplate", menuName = "GameData/ItemTemplate")]
public class ItemTemplate_So : ScriptableObject
{
    public int ItemID;   // JSON 키값
    public GameObject prefab;  // 미리 연결된 프리팹
    public Sprite sprite;      // 미리 연결된 스프라이트
}
