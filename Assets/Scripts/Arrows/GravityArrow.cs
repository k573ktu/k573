using UnityEngine;

public class GravityArrow : Arrow
{
    [SerializeField] float maxForce;
    [SerializeField] int otherPlanetIndex;

    protected override void UpdateArrowDirection()
    {
        if (!analyzedObject.GetComponent<PlanetMovement>().otherPlanets[otherPlanetIndex].gameObject.activeSelf)
        {
            hide();
            return;
        }
        Vector2 curr = analyzedObject.GetComponent<PlanetMovement>().allForces[otherPlanetIndex];
        arrowDirection = Mathf.InverseLerp(0, maxForce, Mathf.Min(curr.magnitude, maxForce)) * maxArrowDisplayLength * curr.normalized;
        if (StaticStorage.arrowLength.HasValue)
        {
            arrowDirection *= StaticStorage.arrowLength.Value;
        }
        
    }
}
