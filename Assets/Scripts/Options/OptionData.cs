using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionData : MonoBehaviour
{
    [SerializeField] public Rigidbody2D analyzedObject;

    [SerializeField] protected string OptionName;
    [SerializeField] protected string Metric;
    [SerializeField] protected Slider slider;
    [SerializeField] protected TextMeshProUGUI text;
    [SerializeField] protected bool DisableOnSimulationStart = false;

    protected virtual void OnValueChanged(float value) { }

    protected virtual void UpdateDisplay(float value)
    {
        text.text = OptionName + ": " + slider.value.ToString() + Metric;
    }

    protected virtual void Start()
    {
        if (DisableOnSimulationStart)
        {
            GameManager.inst.RegisterOptionData(this);
        }

        OnUpdated();
    }

    public virtual void OnSimulationStarted()
    {
        if (DisableOnSimulationStart && slider!=null)
        {
            slider.interactable = false;
        }
    }

    public virtual void OnSimulationStopped()
    {
        if (DisableOnSimulationStart && slider != null)
        {
            slider.interactable = true;
        }
    }

    public void OnUpdated()
    {
        if (slider != null)
        {
            UpdateDisplay(slider.value);
            OnValueChanged(slider.value);
        }
    }
}
