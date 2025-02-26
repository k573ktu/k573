using UnityEngine;

public class WeightOptionData : OptionData
{
    [SerializeField] GameObject analyzedObject;

    protected override void OnValueChanged(float value)
    {
        analyzedObject.GetComponent<Rigidbody2D>().mass = value;
    }
}
