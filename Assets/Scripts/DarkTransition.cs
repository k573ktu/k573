using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class DarkTransition : MonoBehaviour
{
    public static DarkTransition inst;

    [SerializeField] Image darknessObject;

    private void Awake()
    {
        if (inst == null) inst = this;
    }

    public void BlackDisappear(Action onDone = null, bool useTimeScale = true)
    {
        if (darknessObject.gameObject == null) return;
        darknessObject.color = new Color(darknessObject.color.r, darknessObject.color.g, darknessObject.color.b, 1);
        darknessObject.gameObject.SetActive(true);
        darknessObject.DOKill();
        darknessObject.DOFade(0, 0.4f).SetEase(Ease.InOutCubic).SetUpdate(!useTimeScale).OnComplete(() => {
            darknessObject.gameObject.SetActive(false);
            if (onDone != null) onDone.Invoke();
        });
    }

    public void BlackAppear(Action onDone = null, bool useTimeScale = true)
    {
        if (darknessObject.gameObject == null) return;
        darknessObject.color = new Color(darknessObject.color.r, darknessObject.color.g, darknessObject.color.b, 0);
        darknessObject.gameObject.SetActive(true);
        darknessObject.DOKill();
        darknessObject.DOFade(1, 0.4f).SetEase(Ease.InOutCubic).SetUpdate(!useTimeScale).OnComplete(() => {
            if (onDone != null) onDone.Invoke();
        });
    }

    void Start()
    {
        BlackDisappear();
    }
}
