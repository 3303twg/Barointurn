using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : Button
{
    protected override void Awake()
    {
        base.Awake();
        //onClick.AddListener(() => UIManager.Instance.ToggleUI(gameObject.name));
    }
    public override void OnSubmit(BaseEventData eventData)
    {
        if (eventData is PointerEventData pointerEventData)
        {
            return;
        }

    }
}
