using UnityEngine;

public class VelocityOptionData : OptionData
{
    [SerializeField] Rigidbody2D analyzedObject;

    protected override void OnValueChanged(float value)
    {
        analyzedObject.GetComponent<CarMovement>().ChangeSpeed(Vector2.right * value);
    }
}
