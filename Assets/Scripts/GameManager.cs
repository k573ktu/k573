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

    public List<Vector2> objStartPositions;

    public bool simPlaying;
    bool paused;

    bool inPauseMeniu;
    [SerializeField] GameObject pauseMeniu;

    private void Awake()
    {
        if (inst == null) inst = this;
        SimulationArrows = new List<Arrow>();
    }

    void Start()
    {
        simPlaying = false;
        paused = false;
        PauseSimulationButton.GetComponent<Button>().interactable = false;
        PrimaryPauseColor = PauseSimulationButton.color;
        inPauseMeniu = false;

        foreach (var i in SimulationObjects)
        {
            Rigidbody2D currRigid;

            if (i.TryGetComponent(out currRigid))
            {
                currRigid.simulated = false;
                objStartPositions.Add(new Vector2());
            }
            else
            {
                foreach (var j in i.GetComponentsInChildren<Rigidbody2D>())
                {
                    j.simulated = false;
                    objStartPositions.Add(new Vector2());
                }
            }
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

        int counter = 0;

        foreach (var i in SimulationObjects)
        {
            Rigidbody2D currRigid;

            if (i.TryGetComponent(out currRigid))
            {
                currRigid.simulated = true;
                objStartPositions[counter] = i.transform.position;
                counter++;
            }
            else
            {
                foreach (var j in i.GetComponentsInChildren<Rigidbody2D>())
                {
                    j.simulated = true;
                    objStartPositions[counter] = j.transform.position;
                    counter++;
                }
            }

            SimulationStart start;
            if(i.TryGetComponent(out start))
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

        int counter = 0;

        foreach (var i in SimulationArrows)
        {
            i.hide();
        }

        foreach (var i in SimulationObjects)
        {

            Rigidbody2D currRigid;
            

            if (i.TryGetComponent(out currRigid))
            {
                currRigid.simulated = false;
                currRigid.linearVelocity = Vector2.zero;
                currRigid.angularVelocity = 0;
                i.transform.position = objStartPositions[counter];
                counter++;
                i.transform.rotation = Quaternion.identity;
            }
            else
            {
                foreach (var j in i.GetComponentsInChildren<Rigidbody2D>())
                {
                    j.simulated = false;
                    j.linearVelocity = Vector2.zero;
                    j.angularVelocity = 0;
                    j.transform.position = objStartPositions[counter];
                    counter++;
                    j.transform.rotation = Quaternion.identity;
                }
            }
            
            
            SimulationStart start;
            if (i.TryGetComponent(out start))
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

    public void BackToMain()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void PauseOn()
    {
        pauseMeniu.SetActive(true);
        inPauseMeniu = true;
        Time.timeScale = 0;
    }

    void PauseOff()
    {
        pauseMeniu.SetActive(false);
        inPauseMeniu = false;
        if (!paused)
        {
            Time.timeScale = 1;
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
        }else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inPauseMeniu)
            {
                inPauseMeniu = false;
                PauseOff();
            }
            else
            {
                BackToMain();
            } 
        }
    }
}
