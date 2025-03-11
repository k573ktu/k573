using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] List<GameObject> SimulationObjects;

    [SerializeField] Image PlaySimulationButton;
    [SerializeField] Image PauseSimulationButton;

    [SerializeField] Sprite PlaySimulationIcon;
    [SerializeField] Sprite StopSimulationIcon;

    [SerializeField] Color SelectedPauseColor;
    Color PrimaryPauseColor;

    Vector2[] objStartPositions;

    bool simPlaying;
    bool paused;

    void Start()
    {
        simPlaying = false;
        paused = false;
        objStartPositions = new Vector2[SimulationObjects.Count];

        PauseSimulationButton.GetComponent<Button>().interactable = false;

        PrimaryPauseColor = PauseSimulationButton.color;

        for (int i = 0; i < SimulationObjects.Count; i++)
        {
            SimulationObjects[i].GetComponent<Rigidbody2D>().simulated = false;
        }
    }

    void StartSimulation()
    {
        simPlaying = true;
        PlaySimulationButton.transform.GetChild(0).GetComponent<Image>().sprite = StopSimulationIcon;
        PauseSimulationButton.GetComponent<Button>().interactable = true;
        FormulaManager.inst.StartAllFormulas();
        for (int i = 0;i < SimulationObjects.Count;i++)
        {
            objStartPositions[i] = SimulationObjects[i].transform.position;
            SimulationObjects[i].GetComponent<Rigidbody2D>().simulated = true;
            SimulationStart start;
            if(SimulationObjects[i].TryGetComponent(out start))
            {
                start.OnSimulationStart();
            }
        }
    }

    void StopSimulation()
    {
        simPlaying = false;
        PlaySimulationButton.transform.GetChild(0).GetComponent<Image>().sprite = PlaySimulationIcon;
        UnpauseSimulation();
        PauseSimulationButton.GetComponent<Button>().interactable = false;
        FormulaManager.inst.StopAllFormulas();
        for (int i = 0; i < SimulationObjects.Count; i++)
        {
            SimulationObjects[i].GetComponent<Rigidbody2D>().simulated = false;
            SimulationObjects[i].GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            SimulationObjects[i].GetComponent<Rigidbody2D>().angularVelocity = 0;
            SimulationObjects[i].transform.position = objStartPositions[i];
            SimulationStart start;
            if (SimulationObjects[i].TryGetComponent(out start))
            {
                start.OnSimulationStop();
            }
        }
    }

    void PauseSimulation()
    {
        Time.timeScale = 0;
        PauseSimulationButton.color = SelectedPauseColor;
        paused = true;
    }

    void UnpauseSimulation()
    {
        Time.timeScale = 1;
        PauseSimulationButton.color = PrimaryPauseColor;
        paused = false;
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

    public void UpdatePause()
    {
        if (!simPlaying) return;
        if (!paused)
        {
            PauseSimulation();
        }
        else
        {
            UnpauseSimulation();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateSimulation();
        }else if (Input.GetKeyDown(KeyCode.P))
        {
            UpdatePause();
        }
    }
}
