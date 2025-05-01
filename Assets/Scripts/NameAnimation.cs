using UnityEngine;
using DG.Tweening;

public class NameAnimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RectTransform rect = GetComponent<RectTransform>();

        rect.localRotation = Quaternion.Euler(0, 0, -5);

        rect.DOLocalRotate(new Vector3(0, 0, 5), 3).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);

        rect.DOScale(1.05f, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);

    }

}
