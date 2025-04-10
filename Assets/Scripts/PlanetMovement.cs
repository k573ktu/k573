using UnityEngine;

public class PlanetMovement : SimulationStart
{
    const double G = 0.0667428; // Scaled gravitational constant
    //const double G = 6.67428; // Scaled gravitational constant

    bool started;

    [SerializeField] Rigidbody2D otherPlanet;
    [SerializeField] Rigidbody2D sun;
    [SerializeField] Vector2 startVelocity;
    Rigidbody2D thisPlanet;

    public Vector2 currForce;
    public Vector2 currSunForce;

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
        if (started && otherPlanet != null)
        {
            Vector2 direction = (otherPlanet.position - thisPlanet.position).normalized;
            float distance = Vector2.Distance(thisPlanet.position, otherPlanet.position);

            Vector2 sundirection = (sun.position - thisPlanet.position).normalized;
            float sundistance = Vector2.Distance(thisPlanet.position, sun.position);

            if (distance > 0)
            {
                double forceMagnitude = G * (thisPlanet.mass * otherPlanet.mass) / (distance * distance);
                Vector2 force = direction * (float)forceMagnitude;

                thisPlanet.AddForce(force);

                currForce = force;
            }

            if (sundistance > 0)
            {
                double forceMagnitude = G * (thisPlanet.mass * sun.mass) / (sundistance * sundistance);
                Vector2 force = sundirection * (float)forceMagnitude;

                thisPlanet.AddForce(force);

                currSunForce = force;
            }
        }
    }
}
