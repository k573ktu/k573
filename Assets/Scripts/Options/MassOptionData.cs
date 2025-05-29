using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MassOptionData : OptionData
{
    protected override void OnValueChanged(float value)
    {
        analyzedObject.GetComponent<Rigidbody2D>().mass = value;
    }
}
