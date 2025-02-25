using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] List<GameObject> SimulationObjects;

    Vector2[] objStartPositions;

    bool simPlaying;

    void Start()
    {
        simPlaying = false;
        objStartPositions = new Vector2[SimulationObjects.Count];

        for (int i = 0; i < SimulationObjects.Count; i++)
        {
            SimulationObjects[i].GetComponent<Rigidbody2D>().simulated = false;
        }
    }

    void StartSimulation()
    {
        simPlaying = true;
        for(int i = 0;i < SimulationObjects.Count;i++)
        {
            objStartPositions[i] = SimulationObjects[i].transform.position;
            SimulationObjects[i].GetComponent<Rigidbody2D>().simulated = true;
        }
    }

    void StopSimulation()
    {
        simPlaying = false;
        for (int i = 0; i < SimulationObjects.Count; i++)
        {
            SimulationObjects[i].GetComponent<Rigidbody2D>().simulated = false;
            SimulationObjects[i].GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            SimulationObjects[i].GetComponent<Rigidbody2D>().angularVelocity = 0;
            SimulationObjects[i].transform.position = objStartPositions[i];
        }
    }

    public void UpdateSimulation()
    {
        if (!simPlaying)
        {
            StartSimulation();
        }
        else
        {
            StopSimulation();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateSimulation();
        }
    }
}
