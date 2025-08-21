using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSlot : MonoBehaviour
{
    public EnemyStat_So enemyData;

    public CollectSlotUI slotUI;



    public void RefrashIcon()
    {
        slotUI.icon.sprite = enemyData.sprite;
    }

}
