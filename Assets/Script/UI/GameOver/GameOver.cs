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

        // UI�� ��ǥ ��ġ ����
        targetPos = uiElement.localPosition;
    }

    private void OnEnable()
    {
        GameManager.Instance.inputController.OnPuase();
        IntroAnimation();
    }

    public void IntroAnimation()
    {
        // ���� Ʈ�� ����
        uiElement.DOKill();

        // ���� ��ġ: ���� ����� ����
        Vector3 startPos = targetPos + new Vector3(0, dropDistance, 0);
        uiElement.localPosition = startPos;

        // 1. ������ �������� (���� �ӵ�)
        float dropDuration = duration; // �������� �ð�
        uiElement.DOLocalMoveY(targetPos.y, dropDuration) // ��ǥ���� �ణ ������ ����
                 .SetEase(Ease.OutQuad)
                 .SetUpdate(true) // Ÿ�ӽ����� ����
                /*.OnComplete(() =>
                 {
                     // 2. �ణ ƨ�ܼ� ��ǥ ��ġ
                     float bounceDuration = duration * 0.1f; // ƨ��� �ӵ��� ������
                     uiElement.DOLocalMoveY(targetPos.y, bounceDuration)
                              .SetEase(Ease.OutBack)
                              .SetUpdate(true);
                 }*/;
    }

}
