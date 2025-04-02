using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene loading

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject MainUi;
    [SerializeField] GameObject SelectionUi;
    [SerializeField] GameObject OptionsUi;

    public static UiManager inst;

    private void Awake()
    {
        if (inst == null) inst = this;
    }

    private void Start()
    {
        GoMain();
    }

    public void GoMain()
    {
        MainUi.SetActive(true);
        SelectionUi.SetActive(false);
        OptionsUi.SetActive(false);
    }

    public void GoSelection()
    {
        MainUi.SetActive(false);
        OptionsUi.SetActive(false);
        SelectionUi.SetActive(true);
    }

    public void GoOptions()
    {
        MainUi.SetActive(false);
        OptionsUi.SetActive(true);
        SelectionUi.SetActive(false);
    }

    public void GoTheoryScene()
    {
        SceneManager.LoadScene("TheoryScene"); // Name must match exactly
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoMain();
        }
    }
}
