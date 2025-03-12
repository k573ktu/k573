using UnityEngine;

public class VelocityOptionData : OptionData
{
    [SerializeField] Rigidbody2D analyzedObject;

    float currSpeed;
    protected override void Start()
    {
        currSpeed = 0;
        base.Start();
    }

    private void Update()
    {
        analyzedObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(currSpeed, 0);
    }

    protected override void OnValueChanged(float value)
    {
        currSpeed = value;
    }
}
