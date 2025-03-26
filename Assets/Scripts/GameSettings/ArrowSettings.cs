using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class AudioSettings : Settings
{

    protected override void Start()
    {
        if (!StaticStorage.arrowLength.HasValue)
        {
            slider.value = defaultValue;
            StaticStorage.arrowLength = (float)slider.value * 0.1f;
        }
        else
        {
            slider.value = (int)(StaticStorage.arrowLength * 10);
        }
        text.text = string.Format("{0}: {1:0.00}{2}", SettingsName, slider.value * 0.1f, metric);
    }
    public override void ChangeSetting()
    {
        StaticStorage.arrowLength = slider.value * 0.1f;
        text.text = string.Format("{0}: {1:0.00}{2}",SettingsName, slider.value * 0.1f, metric);
    }
}
