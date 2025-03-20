using UnityEngine;

public class GravityArrow : Arrow
{
    [SerializeField] float maxForce;

    protected override void UpdateArrowDirection()
    {
        Vector2 curr = analyzedObject.GetComponent<PlanetMovement>().currForce;
        arrowDirection = curr.normalized * Mathf.Min(curr.magnitude, maxForce);
    }
}
