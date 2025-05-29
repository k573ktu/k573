using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public List<GameObject> SimulationObjects;

    List<OptionData> OptionDataObjects;

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
    bool goingToMain;

    bool inPauseMeniu;
    [SerializeField] GameObject pauseMeniu;

    public bool displayScene;

    private void Awake()
    {
        if (inst == null) inst = this;
        SimulationArrows = new List<Arrow>();
        OptionDataObjects = new List<OptionData>();
    }

    void Start()
    {
        if(SceneManager.loadedSceneCount > 1)
        {
            displayScene = true;
            foreach(GameObject obj in gameObject.scene.GetRootGameObjects()) 
            { 
                if(obj.name == "Canvas")
                {
                    obj.SetActive(false);
                }
                if(obj.name == "EventSystem")
                {
                    Destroy(obj);
                }
                Camera cam;
                if(obj.TryGetComponent(out cam))
                {
                    cam.GetComponent<AudioListener>().enabled = false;
                }
            }
        }
        else
        {
            displayScene = false;
        }
            simPlaying = false;
        paused = false;
        
        PauseSimulationButton.GetComponent<Button>().interactable = false;
        PrimaryPauseColor = PauseSimulationButton.GetComponent<Button>().colors.normalColor;
        inPauseMeniu = false;
        goingToMain = false;
        Time.timeScale = 1;

        foreach (var i in SimulationObjects)
        {
            if (i == null)
            {
                objStartPositions.Add(new Vector2());
            }
            else
            {
                StopObjectSimulation(i);
            }
        }
    }

    public void StopObjectSimulation(GameObject obj)
    {
        Rigidbody2D currRigid;

        if (obj.TryGetComponent(out currRigid))
        {
            currRigid.bodyType = RigidbodyType2D.Static;
            objStartPositions.Add(new Vector2());
        }
        else
        {
            foreach (var j in obj.GetComponentsInChildren<Rigidbody2D>())
            {
                j.bodyType = RigidbodyType2D.Static;
                objStartPositions.Add(new Vector2());
            }
        }
    }

    public void RegisterOptionData(OptionData data)
    {
        OptionDataObjects.Add(data);
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

        foreach (var i in OptionDataObjects)
        {
            i.OnSimulationStarted();
        }

        int counter = 0;

        foreach (var i in SimulationObjects)
        {
            Rigidbody2D currRigid;

            if (i.TryGetComponent(out currRigid))
            {
                currRigid.bodyType = RigidbodyType2D.Dynamic;
                objStartPositions[counter] = i.transform.position;
                counter++;
            }
            else
            {
                foreach (var j in i.GetComponentsInChildren<Rigidbody2D>())
                {
                    j.bodyType = RigidbodyType2D.Dynamic;
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

        foreach (var i in OptionDataObjects)
        {
            i.OnSimulationStopped();
        }

        foreach (var i in SimulationArrows)
        {
            i.hide();
        }

        foreach (var i in SimulationObjects)
        {

            Rigidbody2D currRigid;
            

            if (i.TryGetComponent(out currRigid))
            {
                currRigid.linearVelocity = Vector2.zero;
                currRigid.angularVelocity = 0;
                currRigid.bodyType = RigidbodyType2D.Static;
                i.transform.position = objStartPositions[counter];
                counter++;
                i.transform.rotation = Quaternion.identity;
            }
            else
            {
                foreach (var j in i.GetComponentsInChildren<Rigidbody2D>())
                {
                    j.linearVelocity = Vector2.zero;
                    j.angularVelocity = 0;
                    j.bodyType = RigidbodyType2D.Static;
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
        var col = PauseSimulationButton.GetComponent<Button>().colors;
        col.normalColor = SelectedPauseColor;
        PauseSimulationButton.GetComponent<Button>().colors = col;
        paused = true;
    }

    void UnpauseSimulation()
    {
        Time.timeScale = 1;
        var col = PauseSimulationButton.GetComponent<Button>().colors;
        col.normalColor = PrimaryPauseColor;
        PauseSimulationButton.GetComponent<Button>().colors = col;
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
        if (goingToMain) return;
        goingToMain = true;
        DarkTransition.inst.BlackAppear(()=>SceneManager.LoadScene("MainScene"), false);
    }

    public void PauseOn()
    {
        pauseMeniu.SetActive(true);
        inPauseMeniu = true;
        Time.timeScale = 0;
    }

    public void PauseOff()
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
        if (displayScene) return;
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
