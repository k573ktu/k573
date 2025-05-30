using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class TaskManager : MonoBehaviour
{
    public GameManager GameManager;
    public GameObject topBox;
    public GameObject bottomBox;
    public GameObject vehicle;
    public GameObject planet1;
    public GameObject planet2;
    public GameObject planet3;
    public bool burntPlanet = false;
    public bool collidedPlanets = false;
    public TextMeshProUGUI task1text, task2twxt, task3text;
    private Vector2 topBoxStart;
    private Vector2 bottomBoxStart;

    private List<Task> tasks = new List<Task>();

    void Start()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        
        StartCoroutine(UpdateTasksPeriodically(1f));

        if (sceneIndex == 2)
        {
            StartCoroutine(DelayedSetVehicle(0.5f));
            topBoxStart = topBox.transform.position;
            bottomBoxStart = bottomBox.transform.position;

            tasks.Add(new Task("Avarija nenumušant dėžių", CrashWithoutKnocking));
            tasks.Add(new Task("Avarija numušant tik viršutinę dėžę", OnlyTopBoxKnocked));
            tasks.Add(new Task("Avarija numušant abi dėžes", BothBoxesKnocked));
        }
        else if (sceneIndex == 3)
        {
            tasks.Add(new Task("Išmesti planeta iš orbitos", ThrowPlanetOutOfOrbit));
            tasks.Add(new Task("Sudeginti planetą", BurnPlanet));
            tasks.Add(new Task("Dviejų planetų susidūrimas", CollidePlanets));
        }
        UpdateTaskUI();
    }
    private IEnumerator UpdateTasksPeriodically(float intervalSeconds)
    {
        while (true)
        {
            foreach (Task task in tasks)
            {
                task.UpdateTask();
            }

            UpdateTaskUI();

            yield return new WaitForSeconds(intervalSeconds);
        }
    }
    private IEnumerator DelayedSetVehicle(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);

        if (GameManager.SimulationObjects != null &&
            GameManager.SimulationObjects.Count > 2 &&
            GameManager.SimulationObjects[2] != null)
        {
            vehicle = GameManager.SimulationObjects[2];
        }
        else
        {
            Debug.LogWarning("Vehicle object not found in SimulationObjects[2]");
        }
    }
    /*
    void Update()
    {
        foreach (Task task in tasks)
        {
            task.UpdateTask();
        }

        for (int i = 0; i < tasks.Count; i++)
        {
            tasks[i].UpdateTask();
        }

        UpdateTaskUI();
    }
    */
    void UpdateTaskUI()
    {
        if (task1text != null && tasks.Count > 0)
            task1text.text = "Užduotis 1: " + tasks[0].GetStatusText();

        if (task2twxt != null && tasks.Count > 1)
            task2twxt.text = "Užduotis 2: " + tasks[1].GetStatusText();

        if (task3text != null && tasks.Count > 2)
            task3text.text = "Užduotis 3: " + tasks[2].GetStatusText();
    }
    // Sim1 logic
    bool CrashWithoutKnocking()
    {
        return HasCrashed() && !HasMoved(topBox, topBoxStart) && !HasMoved(bottomBox, bottomBoxStart);
    }

    bool OnlyTopBoxKnocked()
    {
        return HasMoved(topBox, topBoxStart) && !HasMoved(bottomBox, bottomBoxStart);
    }

    bool BothBoxesKnocked()
    {
        return HasMoved(topBox, topBoxStart) && HasMoved(bottomBox, bottomBoxStart);
    }

    bool HasCrashed()
    {
        return vehicle.GetComponentInChildren<CarMovement>().hit;
        //return vehicle.GetComponentInChildren<Rigidbody2D>().linearVelocity.magnitude < 0.001f && GameManager.simPlaying == true;
    }

    bool HasMoved(GameObject obj, Vector2 start)
    {
        return Vector2.Distance(obj.transform.position, start) > 0.5f;
    }
    bool HasMoved2(GameObject obj, Vector2 start)
    {
        return Vector2.Distance(obj.transform.position, start) > 300f;
    }

    // Sim2 logic
    bool ThrowPlanetOutOfOrbit() 
    { 
        return HasMoved2(planet1, new Vector2(0,0)) || HasMoved2(planet2, new Vector2(0, 0)) || HasMoved2(planet3, new Vector2(0, 0));
    }
    bool BurnPlanet() 
    { 
        return burntPlanet; 
    }
    bool CollidePlanets() 
    { 
        return collidedPlanets; 
    }
}
