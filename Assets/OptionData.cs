using UnityEngine;
using UnityEngine.UI;

public class OptionData : MonoBehaviour
{
    [SerializeField] string OptionName;
    [SerializeField] Slider slider;

    private void Start()
    {
        OptionsManager.inst.InsertOption(OptionName, slider.minValue);
    }

    public void OnUpdated()
    {
        OptionsManager.inst.InsertOption(OptionName, slider.value);
    }
}
