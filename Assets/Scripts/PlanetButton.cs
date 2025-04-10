using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] PlanetOptionData planetOptionData;
    public void OnPointerDown(PointerEventData eventData)
    {
        planetOptionData.PickObject();
    }
}
