using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class MainButtonSelected : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Visual Settings")]
    [SerializeField][Range(0, 2)] float onHoverMultiplyer;
    [Header("Sound Settings")]
    [SerializeField] private string hoverSoundName = "ButtonHover";
    [SerializeField] private string clickSoundName = "ButtonClick";
    [Range(0, 1)] public float hoverVolume = 1f;
    [Range(0, 1)] public float clickVolume = 1f;
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
        PlaySound(hoverSoundName, hoverVolume);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rect.DOKill();
        rect.DOScale(oldSize, 0.1f).SetEase(Ease.InOutCubic);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlaySound(clickSoundName, clickVolume);
    }

    private void PlaySound(string soundName, float volume)
    {
        if (!string.IsNullOrEmpty(soundName) && SoundManager.Instance != null)
        {
            var sound = SoundManager.Instance.GetSound(soundName);
            if (sound != null)
            {
                sound.ChangeVolume(volume).PlayOneShot();
            }
        }
    }
}