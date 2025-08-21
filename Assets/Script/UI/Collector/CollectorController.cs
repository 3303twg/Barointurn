using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorController : MonoBehaviour
{
    public CollectData_So collectData;
    public Transform contents;
    public GameObject slotPrefab;

    public CollectorUI collectorUI;

    private void Awake()
    {
        foreach(var data in collectData.enemyData)
        {
            GameObject go = Instantiate(slotPrefab, contents);
            CollectSlot slot = go.GetComponent<CollectSlot>();
            slot.enemyData = data;
            slot.RefrashIcon();
            slot.slotUI.btn.onClick.AddListener(() => collectorUI.RefrashData(slot.enemyData));
        }
    }

}
