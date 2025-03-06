using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlanetMovement : SimulationStart
{
    const double G = 6.67428;

    bool started;

    [SerializeField] Rigidbody2D otherPlanet;
    Rigidbody2D thisPlanet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        started = false;

        thisPlanet = GetComponent<Rigidbody2D>();
    }

    public override void OnSimulationStart()
    {
        started = true;
    }

    public override void OnSimulationStop()
    {
        started = false;
        GetComponent<TrailRenderer>().Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            double force = G * (thisPlanet.mass * otherPlanet.mass) / Mathf.Pow(Vector2.Distance(transform.position, otherPlanet.gameObject.transform.position),2)/ thisPlanet.mass;

            Vector3 dir = (otherPlanet.gameObject.transform.position - transform.position).normalized;

            thisPlanet.linearVelocity += (Vector2)dir * (float)force;
        }
    }
}
