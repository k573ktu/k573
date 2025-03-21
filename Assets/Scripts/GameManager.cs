using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] List<GameObject> SimulationObjects;

    [SerializeField] List<Arrow> SimulationArrows;

    [SerializeField] Image PlaySimulationButton;
    [SerializeField] Image PauseSimulationButton;

    [SerializeField] Sprite PlaySimulationIcon;
    [SerializeField] Sprite StopSimulationIcon;

    [SerializeField] Color SelectedPauseColor;
    Color PrimaryPauseColor;

    Vector2[] objStartPositions;

    bool simPlaying;
    bool paused;

    private void Awake()
    {
        if (inst == null) inst = this;
        SimulationArrows = new List<Arrow>();
    }

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

        foreach(var i in SimulationArrows)
        {
            i.show();
        }

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

        foreach (var i in SimulationArrows)
        {
            i.hide();
        }

        for (int i = 0; i < SimulationObjects.Count; i++)
        {
            SimulationObjects[i].GetComponent<Rigidbody2D>().simulated = false;
            SimulationObjects[i].GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            SimulationObjects[i].GetComponent<Rigidbody2D>().angularVelocity = 0;
            SimulationObjects[i].transform.position = objStartPositions[i];
            SimulationObjects[i].transform.rotation = Quaternion.identity;
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

    public void RegisterArrow(Arrow arrow)
    {
        SimulationArrows.Add(arrow);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateSimulation();
        }else if (Input.GetKeyDown(KeyCode.P))
        {
            UpdatePause();
        }else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
