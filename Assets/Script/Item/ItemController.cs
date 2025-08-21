using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour, Iinteractable
{
    public ItemData_So itemData;

    public BaseEffect item;

    private void Awake()
    {
        item = GetComponent<BaseEffect>();
        EventBus.Subscribe("GameOver", ReturnPool);
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe("GameOver", ReturnPool);
    }
    public void OnInteract()
    {
        if (item != null)
        {
            item.Effect();
        }
        //��� �ϴ� Ǯ�� ��� �н�
        Destroy(gameObject);
    }


    public void ReturnPool(object obj)
    {
        Destroy(gameObject);
    }
}
