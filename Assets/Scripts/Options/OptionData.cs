using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionData : MonoBehaviour
{
    [SerializeField] string OptionName;
    [SerializeField] string Metric;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI text;

    protected virtual void OnValueChanged(float value) { }

    private void Start()
    {
        OptionsManager.inst.InsertOption(OptionName, slider.minValue);
        text.text = OptionName + ": " + slider.minValue.ToString() + Metric;
        OnValueChanged(slider.minValue);
    }

    public void OnUpdated()
    {
        OptionsManager.inst.InsertOption(OptionName, slider.value);
        text.text = OptionName + ": " + slider.value.ToString() + Metric;
        OnValueChanged(slider.value);
    }
}
