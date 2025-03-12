using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionData : MonoBehaviour
{
    [SerializeField] protected string OptionName;
    [SerializeField] protected string Metric;
    [SerializeField] protected Slider slider;
    [SerializeField] protected TextMeshProUGUI text;

    protected virtual void OnValueChanged(float value) { }

    protected virtual void UpdateDisplay(float value)
    {
        text.text = OptionName + ": " + slider.value.ToString() + Metric;
    }

    protected virtual void Start()
    {
        OnUpdated();
    }

    public void OnUpdated()
    {
        OptionsManager.inst.InsertOption(OptionName, slider.value);
        UpdateDisplay(slider.value);
        OnValueChanged(slider.value);
    }
}
