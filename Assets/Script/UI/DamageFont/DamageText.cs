using TMPro;
using UnityEngine;
using DG.Tweening;

public class DamageText : MonoBehaviour, IPoolObject
{
    public TextMeshProUGUI Bg;
    public TextMeshProUGUI Text;

    [Header("Motion Settings")]
    public float lifeTime = 1.2f;
    public float upwardForce = 2.5f;
    public float horizontalForce = 4.5f;
    public float gravity = -6f;

    [Header("Scale Settings")]
    public float startScale = 1.3f;
    public float endScale = 0.3f;
    public float scaleShrinkTime = 1f;

    [Header("Fade Settings")]
    public float fadeDuration = 0.5f;

    private float timer;
    private Vector3 velocity;

    string originName;

    private void Awake()
    {
        EventBus.Subscribe("GameOver", ReturnPool);
    }
    public void Init(float value)
    {
        timer = 0f;

        // 랜덤 방향 velocity 초기화
        float x = Random.Range(-horizontalForce, horizontalForce);
        velocity = new Vector3(x, upwardForce, 0f);

        // 텍스트 지정
        string str = value.ToString("F1");
        Text.text = str;
        Bg.text = str;
        SetAlpha(1f);

        // 트윈 초기화 (기존 트윈이 남아있을 수 있으므로 Kill 후 다시 시작)
        transform.DOKill();

        transform.localScale = Vector3.zero;
        transform.DOScale(startScale, 0.15f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            transform.DOScale(endScale, scaleShrinkTime).SetEase(Ease.InQuad);
        });

        Text.DOFade(0f, fadeDuration).SetDelay(lifeTime - fadeDuration);
        Bg.DOFade(0f, fadeDuration).SetDelay(lifeTime - fadeDuration);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // 포물선 이동
        velocity += Vector3.up * gravity * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        // 항상 카메라 바라보기
        transform.forward = Camera.main.transform.forward;

        if (timer >= lifeTime)
            ReturnPool();
    }

    private void SetAlpha(float alpha)
    {
        var c1 = Text.color; c1.a = alpha; Text.color = c1;
        var c2 = Bg.color; c2.a = alpha; Bg.color = c2;
    }

    public void InitName(string name)
    {
        originName = name;
    }

    public void ReturnPool()
    {

        transform.DOKill();
        ObjectPoolManager.Return(gameObject, originName);
    }

    public void ReturnPool(object obj)
    {

        transform.DOKill();
        ObjectPoolManager.Return(gameObject, originName);
    }
}
