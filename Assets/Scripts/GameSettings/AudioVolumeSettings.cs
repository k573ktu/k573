using UnityEngine;

public class AudioVolumeSettings : Settings
{
    protected override void Start()
    {
        if (!StaticStorage.audioVolume.HasValue)
        {
            slider.value = defaultValue;
            StaticStorage.audioVolume = (float)slider.value / 100;
        }
        else
        {
            slider.value = (int)(StaticStorage.audioVolume*100);
        }
        text.text = SettingsName + ": " + slider.value.ToString() + metric;
    }
    public override void ChangeSetting()
    {
        AudioListener.volume = (float)slider.value/100;
        StaticStorage.audioVolume = (float)slider.value / 100;
        text.text = SettingsName + ": " + slider.value.ToString() + metric;
    }
}
