using UnityEngine;

public class SunGravityArrow : Arrow
{
    [SerializeField] float maxForce;

    protected override void UpdateArrowDirection()
    {
        Vector2 curr = analyzedObject.GetComponent<PlanetMovement>().currSunForce;
        arrowDirection = Mathf.InverseLerp(0, maxForce, Mathf.Min(curr.magnitude, maxForce)) * maxArrowDisplayLength * curr.normalized;
        if (StaticStorage.arrowLength.HasValue)
        {
            arrowDirection *= StaticStorage.arrowLength.Value;
        }
    }
}
