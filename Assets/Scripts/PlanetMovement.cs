using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
public class PlanetMovement : SimulationStart
{
    const double G = 0.0667428;

    bool started;

    bool dead;
    Vector3 lastScale;

    public List<Rigidbody2D> otherPlanets;
    [SerializeField] Rigidbody2D sun;
    [SerializeField] Vector2 startVelocity;
    Rigidbody2D thisPlanet;

    [System.NonSerialized] public Vector2 currForce;
    [System.NonSerialized] public Vector2 currSunForce;
    public Vector2[] allForces;

    const float planetInfluenceFactor = 0.06f; // otherPlanet influence mult
    const float minPlanetDistance = 0.5f; // Minimum safe distance between planets to prevent collapse
    public TaskManager taskMan;

    
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

            Vector2 direction = (sun.transform.position - transform.position).normalized;
            Vector2 rotatedDirection = new Vector2(direction.y, -direction.x);

            thisPlanet.velocity = rotatedDirection * startVelocity.magnitude;
        }
    }

    public override void OnSimulationStop()
    {
        started = false;
        ResetDeath();
        GetComponent<TrailRenderer>().Clear();
    }
    void FixedUpdate()
    {
        if (!started) return;
        if (dead) return;
        if (sun != null)
        {
            ApplyGravity(sun, 1f, out currSunForce);

        }
        for(int i = 0;i<otherPlanets.Count;i++)
        {
            if (otherPlanets[i] != null && otherPlanets[i] != thisPlanet)
            {
                ApplyGravity(otherPlanets[i], planetInfluenceFactor, out allForces[i]);
                currForce += allForces[i];
            }
        }
    }

    void ApplyGravity(Rigidbody2D otherBody, float influenceScale, out Vector2 forceOut)
    {
        if (!otherBody.gameObject.activeSelf)
        {
            forceOut = Vector2.zero;
            return;
        }

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

    public void ResetDeath()
    {
        if (!dead) return;
        dead = false;
        transform.DOKill();
        gameObject.SetActive(true);
        transform.localScale = lastScale;
    }

    void Death()
    {
        if (dead) return;

        dead = true;

        lastScale = gameObject.transform.localScale;

        thisPlanet.velocity = Vector2.zero;

        transform.DOScale(0, 0.2f).SetEase(Ease.OutSine).OnComplete(() => gameObject.SetActive(false));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "sun")
        {
            taskMan.burntPlanet = true;
            Death();
        }

        if (collision.tag == "planet")
        {
            if (collision.GetComponent<Rigidbody2D>().mass < GetComponent<Rigidbody2D>().mass) return;
            taskMan.collidedPlanets = true;
            Death();
        }
    }

}



