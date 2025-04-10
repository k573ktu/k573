using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene loading

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject MainUi;
    [SerializeField] GameObject SelectionUi;
    [SerializeField] GameObject OptionsUi;
    [SerializeField] GameObject TheoryUi;
    [SerializeField] GameObject TestUi;

    public static UiManager inst;

    private void Awake()
    {
        if (inst == null) inst = this;
    }

    private void Start()
    {
        GoMain();
        Time.timeScale = 1;
    }

    public void GoMain()
    {
        MainUi.SetActive(true);
        SelectionUi.SetActive(false);
        OptionsUi.SetActive(false);
        TheoryUi.SetActive(false);
        TestUi.SetActive(false);
    }

    public void GoSelection()
    {
        MainUi.SetActive(false);
        OptionsUi.SetActive(false);
        SelectionUi.SetActive(true);
        TheoryUi.SetActive(false);
        TestUi.SetActive(false);
    }

    public void GoOptions()
    {
        MainUi.SetActive(false);
        OptionsUi.SetActive(true);
        SelectionUi.SetActive(false);
        TheoryUi.SetActive(false);
        TestUi.SetActive(false);
    }

    public void GoTheory()
    {
        MainUi.SetActive(false);
        OptionsUi.SetActive(false);
        SelectionUi.SetActive(false);
        TheoryUi.SetActive(true);
        TestUi.SetActive(false);
    }
    public void GoTest()
    {
        MainUi.SetActive(false);
        OptionsUi.SetActive(false);
        SelectionUi.SetActive(false);
        TheoryUi.SetActive(false);
        TestUi.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!TheoryUi.activeSelf && !TestUi.activeSelf && !MainUi.activeSelf)
            {
                GetComponent<LevelSelectionManager>().unselectCurrentSelected();
                GoMain();
            }
        }
    }
}
