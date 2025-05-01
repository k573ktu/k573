using NUnit.Framework.Internal;
using System.Collections.Generic;
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

    List<MainButtonSelected> mainButtons;

    private void Awake()
    {
        if (inst == null) inst = this;
        mainButtons = new List<MainButtonSelected>();
    }

    private void Start()
    {
        GoMain();
        Time.timeScale = 1;
    }

    public void AssignMainButton(MainButtonSelected code)
    {
        mainButtons.Add(code);
    }

    public void ResetButtons()
    {
        mainButtons.RemoveAll(item => (item == null || item.RectNull()));
        foreach (var i in mainButtons)
        {
            i.ResetSize();
        }
    }

    public void GoMain()
    {
        ResetButtons();
        MainUi.SetActive(true);
        SelectionUi.SetActive(false);
        OptionsUi.SetActive(false);
        TheoryUi.SetActive(false);
        TestUi.SetActive(false);
    }

    public void GoSelection()
    {
        ResetButtons();
        MainUi.SetActive(false);
        OptionsUi.SetActive(false);
        SelectionUi.SetActive(true);
        TheoryUi.SetActive(false);
        TestUi.SetActive(false);
    }

    public void GoOptions()
    {
        ResetButtons();
        MainUi.SetActive(false);
        OptionsUi.SetActive(true);
        SelectionUi.SetActive(false);
        TheoryUi.SetActive(false);
        TestUi.SetActive(false);
    }

    public void GoTheory()
    {
        ResetButtons();
        MainUi.SetActive(false);
        OptionsUi.SetActive(false);
        SelectionUi.SetActive(false);
        TheoryUi.SetActive(true);
        TestUi.SetActive(false);
    }
    public void GoTest()
    {
        ResetButtons();
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
