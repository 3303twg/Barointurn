using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarHandler : MonoBehaviour
{


    public Slider HpBar;
    public Slider BgBar;

    public float duration = 0.7f;


    public void Init()
    {
        RefrashHp(1);
    }

    //���������� ȣ��ɰ�
    //���� ���� ����Ǹ� ������?
    public void RefrashHp(float value)
    {
        //���� �̷��� �ؾ��� ĸó�ϴ���
        UpdateBg(HpBar.value , value);
        HpBar.value = value;
    }

    public void RefrashBg(float value)
    {
        BgBar.value = value;
    }


    //���� �����̶�� ������ �ٲٰ�ͳ�
    public void UpdateBg(float start, float end)
    {
        DoTweenExtensions.TweenFloat(start, end, duration, (a)=> 
        {
            RefrashBg(a);
        });
    }
}
