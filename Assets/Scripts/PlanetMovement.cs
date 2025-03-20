using UnityEngine;

public class PlanetMovement : SimulationStart
{
    const double G = 0.0667428; // Scaled gravitational constant
    //const double G = 6.67428; // Scaled gravitational constant

    bool started;

    [SerializeField] Rigidbody2D otherPlanet;
    Rigidbody2D thisPlanet;

    public Vector2 currForce;

    void Start()
    {
        started = false;
        thisPlanet = GetComponent<Rigidbody2D>();

 
    }

    public override void OnSimulationStart()
    {
        started = true;
        // Set initial tangential velocity for double-helix effect
        if (otherPlanet != null)
        {
            Vector2 direction = (otherPlanet.position - thisPlanet.position).normalized;
            float distance = Vector2.Distance(thisPlanet.position, otherPlanet.position);

            // Compute orbital velocity for stable rotation
            float orbitalSpeed = Mathf.Sqrt((float)(G * otherPlanet.mass / distance));

            // Perpendicular vector for initial tangential velocity
            Vector2 tangent = new Vector2(-direction.y, direction.x);

            // Apply initial velocity
            thisPlanet.linearVelocity = tangent * orbitalSpeed;
            otherPlanet.linearVelocity = -tangent * orbitalSpeed;
        }
    }

    public override void OnSimulationStop()
    {
        started = false;
        GetComponent<TrailRenderer>().Clear();
    }

    void FixedUpdate()
    {
        if (started && otherPlanet != null)
        {
            Vector2 direction = (otherPlanet.position - thisPlanet.position).normalized;
            float distance = Vector2.Distance(thisPlanet.position, otherPlanet.position);

            if (distance > 0)
            {
                // Compute gravitational force
                double forceMagnitude = G * (thisPlanet.mass * otherPlanet.mass) / (distance * distance);
                Vector2 force = (Vector2)direction * (float)forceMagnitude;

                // Apply gravitational force
                thisPlanet.AddForce(force);

                currForce = force;

                // Apply angular velocity for double-helix effect
                float angularSpeed = 0.1f; // Adjust for rotation speed
                thisPlanet.angularVelocity = angularSpeed * Mathf.Rad2Deg;
                otherPlanet.angularVelocity = -angularSpeed * Mathf.Rad2Deg;
            }
        }
    }
}
