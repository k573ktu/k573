using UnityEngine;

public class VelocityArrow : Arrow
{
    [SerializeField] float maxForce;

    protected override void UpdateArrowDirection()
    {
        Vector2 curr = analyzedObject.GetComponent<Rigidbody2D>().linearVelocity;
        arrowDirection = Mathf.InverseLerp(0, maxForce, Mathf.Min(curr.magnitude, maxForce)) * maxArrowDisplayLength * curr.normalized;
    }
}
