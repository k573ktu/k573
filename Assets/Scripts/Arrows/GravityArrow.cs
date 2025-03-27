using UnityEngine;

public class GravityArrow : Arrow
{
    [SerializeField] float maxForce;

    protected override void UpdateArrowDirection()
    {
        Vector2 curr = analyzedObject.GetComponent<PlanetMovement>().currForce;
        arrowDirection = Mathf.InverseLerp(0, maxForce, Mathf.Min(curr.magnitude, maxForce)) * maxArrowDisplayLength * StaticStorage.arrowLength.Value * curr.normalized;
    }
}
