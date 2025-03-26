using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] protected string SettingsName;
    [SerializeField] protected string metric;
    [SerializeField] protected Slider slider;
    [SerializeField] protected TextMeshProUGUI text;
    [SerializeField] protected int defaultValue;

    protected virtual void Start()
    {  }

    public virtual void ChangeSetting() { }
}
