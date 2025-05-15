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
        AvatarController.inst.ShowTextOneTime("Labas! Mano vardas Photonas ir esu čia tam, kad padėčiau Tau dar geriau susipažinti su fizika bei jos subtilybėmis! Šios simuliacijos metu galėsi susipažinti su įvairiais fizikos dėsniais, pritaikydamas juos realaus gyvenimo pavyzdžiuose. Sėkmės!", "selection");
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
        AvatarController.inst.ShowTextOneTime("Kad būtų paprasčiau sekti savo progresą – turiu Tau keletą testukų, kuriuos išsprendęs dar geriau užtikrinsi savo fizikos žinias!", "test");
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
