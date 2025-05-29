using Mono.Cecil.Cil;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject MainUi;
    [SerializeField] private GameObject SelectionUi;
    [SerializeField] private GameObject OptionsUi;
    [SerializeField] private GameObject TheoryUi;
    [SerializeField] private GameObject TestUi;
    [SerializeField] private GameObject TeacherPanel;
    [SerializeField] private Button TeacherButton; // Corrected, no duplicate

    [SerializeField] RectTransform testReview;

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

        // Retrieve the UserType and UserCode from PlayerPrefs
        string userType = PlayerPrefs.GetString("UserType", "unknown");
        string userCode = PlayerPrefs.GetString("UserCode", "unknown");
        Debug.Log($"User Type Retrieved: {userType}");
        Debug.Log($"User Code Retrieved: {userCode}");

        // Show the TeacherButton only for teachers
        if (TeacherButton != null)
        {
            if (userType == "teacher")
            {
                TeacherButton.gameObject.SetActive(true);
                TeacherButton.onClick.AddListener(() =>
                {
                    GoTeacherPanel();

                    // Check if the UserCode is still "unknown"
                    if (userCode == "unknown")
                    {
                        // Attempt to get the correct UserCode from PlayerPrefs
                        userCode = PlayerPrefs.GetString("UserCode", "unknown");

                        // If it is still unknown, log a warning
                        if (userCode == "unknown")
                        {
                            Debug.LogError("No user code found in PlayerPrefs.");
                            return;
                        }

                        // Save the UserCode for future use
                        PlayerPrefs.SetString("UserCode", userCode);
                        PlayerPrefs.Save();
                        Debug.Log($"User Code Saved: {userCode}");
                    }
                });
            }
            else
            {
                TeacherButton.gameObject.SetActive(false);
            }
        }
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
        TeacherPanel.SetActive(false);
        AvatarController.inst.HideText();
    }

    public void GoSelection()
    {
        ResetButtons();
        MainUi.SetActive(false);
        OptionsUi.SetActive(false);
        SelectionUi.SetActive(true);
        TheoryUi.SetActive(false);
        TestUi.SetActive(false);
        AvatarController.inst.ShowTextOneTime("Labas! Mano vardas Fotonas ir esu čia tam, kad padėčiau Tau dar geriau susipažinti su fizika bei jos subtilybėmis! Šios simuliacijos metu galėsi susipažinti su įvairiais fizikos dėsniais, pritaikydamas juos realaus gyvenimo pavyzdžiuose. Sėkmės!", "selection");
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
        AvatarController.inst.ShowTextOneTime("Štai čia gali dar geriau pagilinti savo žinias su teorine medžiaga iš fizikos vadovėlių. Pasirink temą ir tapk tikru fizikos žinovu!", "theory");
    }
    public void GoTest()
    {
        ResetButtons();
        MainUi.SetActive(false);
        OptionsUi.SetActive(false);
        SelectionUi.SetActive(false);
        TheoryUi.SetActive(false);
        TestUi.SetActive(true);
        AvatarController.inst.ShowTextOneTime("Kad būtų paprasčiau sekti savo progresą – turiu Tau keletą testų, kuriuos išsprendęs dar geriau užtikrinsi savo fizikos žinias!", "test");

        testReview.offsetMin = new Vector2(0, -3350);
        testReview.offsetMax = new Vector2(0, -0);
    }
    public void GoTeacherPanel()
    {
        ResetButtons();
        MainUi.SetActive(false);
        SelectionUi.SetActive(false);
        OptionsUi.SetActive(false);
        TheoryUi.SetActive(false);
        TestUi.SetActive(false);
        TeacherPanel.SetActive(true);
    }
    public void BackPressed()
    {
        if (!TheoryUi.activeSelf && !TestUi.activeSelf && !MainUi.activeSelf)
        {
            GetComponent<LevelSelectionManager>().unselectCurrentSelected();
            GoMain();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackPressed();
        }
    }
}
