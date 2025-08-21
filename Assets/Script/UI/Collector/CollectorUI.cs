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
    //�� �̰� �׳� json�������� �ѱ�

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
        nameText.text = "�̸� : " + data.Name;
        descriptionText.text = data.Description;
        hpText.text = "ü�� : " + data.MaxHP.ToString();
        attackText.text = "���ݷ� : " + data.Attack.ToString();
        attackRangeText.text = "���ݹ��� : " + data.AttackRange.ToString();
        attackSpeedText.text = "���ݼӵ� : " + data.AttackSpeed.ToString();
        moveSpeedText.text = "�̵��ӵ� : " + data.MoveSpeed.ToString();
        ExpText.text = "����ġ : " + data.MinExp.ToString() + " ~ " + data.MaxExp.ToString();

    }
}
