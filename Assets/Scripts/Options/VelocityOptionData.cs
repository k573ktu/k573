using UnityEngine;

public class VelocityOptionData : OptionData
{
    protected override void OnValueChanged(float value)
    {
        if (analyzedObject != null)
        {
            analyzedObject.GetComponent<CarMovement>().ChangeSpeed(Vector2.right * value);
        }
    }
}
