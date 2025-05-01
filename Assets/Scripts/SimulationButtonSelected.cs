using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class SimulationButtonSelected : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] [Range(0,2)] float onHoverMultiplyer;

    [SerializeField] bool resetOnClick;

    RectTransform rect;

    Vector3 oldSize, newSize;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        oldSize = rect.localScale;
        newSize = onHoverMultiplyer * oldSize;
    }

    public void ResetSize()
    {
        rect.DOKill();

        rect.localScale = oldSize;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rect.DOKill();

        rect.DOScale(newSize, 0.1f).SetEase(Ease.InOutCubic).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rect.DOKill();

        rect.DOScale(oldSize, 0.1f).SetEase(Ease.InOutCubic).SetUpdate(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(resetOnClick)ResetSize();
    }
}
