using UnityEngine;
using UnityEngine.SceneManagement;

public class TheorySceneManager : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject theoryOnlyUI;

    void Start()
    {
        bool showClean = PlayerPrefs.GetInt("CleanTheory", 0) == 1;

        if (showClean)
        {
            mainUI.SetActive(false);
            theoryOnlyUI.SetActive(true);
            PlayerPrefs.SetInt("CleanTheory", 0); // Reset flag
        }
        else
        {
            mainUI.SetActive(true);
            theoryOnlyUI.SetActive(false);
        }
    }

    public void LoadCleanView()
    {
        PlayerPrefs.SetInt("CleanTheory", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
