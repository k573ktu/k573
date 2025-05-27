using UnityEngine;

public class GravityArrow : Arrow
{
    [SerializeField] float maxForce;

    protected override void UpdateArrowDirection()
    {
        if (!analyzedObject.GetComponent<PlanetMovement>().otherPlanet.gameObject.activeSelf)
        {
            hide();
            return;
        }
        Vector2 curr = analyzedObject.GetComponent<PlanetMovement>().currForce;
        arrowDirection = Mathf.InverseLerp(0, maxForce, Mathf.Min(curr.magnitude, maxForce)) * maxArrowDisplayLength * curr.normalized;
        if (StaticStorage.arrowLength.HasValue)
        {
            arrowDirection *= StaticStorage.arrowLength.Value;
        }
        
    }
}
