using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public RectTransform uiElement;
    public float dropDistance = 200f;
    public float duration = 0.5f;

    public Button btn;
    private Vector3 targetPos;


    private void Awake()
    {
        btn.onClick.AddListener(() => GameManager.Instance.SceneLoad());

        // UI의 목표 위치 저장
        targetPos = uiElement.localPosition;
    }

    private void OnEnable()
    {
        GameManager.Instance.inputController.OnPuase();
        IntroAnimation();
    }

    public void IntroAnimation()
    {
        // 기존 트윈 정리
        uiElement.DOKill();

        // 시작 위치: 위로 띄워서 시작
        Vector3 startPos = targetPos + new Vector3(0, dropDistance, 0);
        uiElement.localPosition = startPos;

        // 1. 위에서 내려오기 (빠른 속도)
        float dropDuration = duration; // 내려오는 시간
        uiElement.DOLocalMoveY(targetPos.y, dropDuration) // 목표보다 약간 위에서 멈춤
                 .SetEase(Ease.OutQuad)
                 .SetUpdate(true) // 타임스케일 무시
                /*.OnComplete(() =>
                 {
                     // 2. 약간 튕겨서 목표 위치
                     float bounceDuration = duration * 0.1f; // 튕기는 속도는 빠르게
                     uiElement.DOLocalMoveY(targetPos.y, bounceDuration)
                              .SetEase(Ease.OutBack)
                              .SetUpdate(true);
                 }*/;
    }

}
