using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectorUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI attackRangeText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI moveSpeedText;
    public TextMeshProUGUI ExpText;
    //아 이거 그냥 json느낌으로 둘까

    private void OnEnable()
    {
        icon.sprite = null;

        nameText.text = string.Empty;
        descriptionText.text = string.Empty;
        hpText.text = string.Empty;
        attackText.text = string.Empty;
        attackRangeText.text = string.Empty;
        attackSpeedText.text = string.Empty;
        moveSpeedText.text = string.Empty;
        ExpText.text = string.Empty;

    }

    public void RefrashData(EnemyStat_So data)
    {
        icon.sprite = data.sprite;
        nameText.text = "이름 : " + data.Name;
        descriptionText.text = data.Description;
        hpText.text = "체력 : " + data.MaxHP.ToString();
        attackText.text = "공격력 : " + data.Attack.ToString();
        attackRangeText.text = "공격범위 : " + data.AttackRange.ToString();
        attackSpeedText.text = "공격속도 : " + data.AttackSpeed.ToString();
        moveSpeedText.text = "이동속도 : " + data.MoveSpeed.ToString();
        ExpText.text = "경험치 : " + data.MinExp.ToString() + " ~ " + data.MaxExp.ToString();

    }
}
