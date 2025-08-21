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

    //실질적으로 호출될곳
    //뭔가 색도 변경되면 좋을듯?
    public void RefrashHp(float value)
    {
        //순서 이렇게 해야함 캡처하던가
        UpdateBg(HpBar.value , value);
        HpBar.value = value;
    }

    public void RefrashBg(float value)
    {
        BgBar.value = value;
    }


    //대충 연출이라는 뜻으로 바꾸고싶네
    public void UpdateBg(float start, float end)
    {
        DoTweenExtensions.TweenFloat(start, end, duration, (a)=> 
        {
            RefrashBg(a);
        });
    }
}
