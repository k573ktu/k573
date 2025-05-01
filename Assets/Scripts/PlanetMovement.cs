using UnityEngine;
public class PlanetMovement : SimulationStart
{
    const double G = 0.0667428;

    bool started;

    [SerializeField] Rigidbody2D otherPlanet;
    [SerializeField] Rigidbody2D sun;
    [SerializeField] Vector2 startVelocity;
    Rigidbody2D thisPlanet;

    public Vector2 currForce;
    public Vector2 currSunForce;


    const float planetInfluenceFactor = 0.02f; // otherPlanet influence mult
    const float minPlanetDistance = 0.5f; // Minimum safe distance between planets to prevent collapse

    void Start()
    {
        started = false;
        thisPlanet = GetComponent<Rigidbody2D>();
    }

    public override void OnSimulationStart()
    {
        if (gameObject.activeSelf)
        {
            started = true;

            thisPlanet.linearVelocity = startVelocity;
        }
    }

    public override void OnSimulationStop()
    {
        started = false;
        GetComponent<TrailRenderer>().Clear();
    }
    void FixedUpdate()
    {
        if (!started) return;
        if (sun != null)
        {
            ApplyGravity(sun, 1f, out currSunForce);

        }
        if (otherPlanet != null && otherPlanet != thisPlanet)
        {
            ApplyGravity(otherPlanet, planetInfluenceFactor, out currForce);
        }
    }

    void ApplyGravity(Rigidbody2D otherBody, float influenceScale, out Vector2 forceOut)
    {
        Vector2 direction = otherBody.position - thisPlanet.position;
        float distanceSqr = direction.sqrMagnitude;

        if (distanceSqr == 0)
        {
            forceOut = Vector2.zero;
            return;
        }

        Vector2 forceDir = direction.normalized;
        float forceMagnitude = (float)(G * thisPlanet.mass * otherBody.mass / distanceSqr);
        Vector2 force = forceDir * forceMagnitude * influenceScale;

        thisPlanet.AddForce(force);
        forceOut = force;
    }

}



