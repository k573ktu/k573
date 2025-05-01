using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MainButtonSelected : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] [Range(0,2)] float onHoverMultiplyer;

    RectTransform rect;

    Vector3 oldSize, newSize;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        oldSize = rect.localScale;
        newSize = onHoverMultiplyer * oldSize;
        UiManager.inst.AssignMainButton(this);
    }

    public bool RectNull()
    {
        return rect == null;
    }

    public void ResetSize()
    {
        rect.DOKill();

        rect.localScale = oldSize;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rect.DOKill();

        rect.DOScale(newSize, 0.1f).SetEase(Ease.InOutCubic);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rect.DOKill();

        rect.DOScale(oldSize, 0.1f).SetEase(Ease.InOutCubic);
    }
}
