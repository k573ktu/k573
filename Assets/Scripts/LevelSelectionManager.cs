using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour
{
    public static LevelSelectionManager inst;

    Leveldata selectedLevelData;

    [SerializeField] GameObject InfoBackground;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;

    [SerializeField] Color selectedButtonColor;
    [SerializeField] Color unselectedButtonColor;

    [SerializeField] GameObject displayObjects;

    bool LevelStarting;

    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        LevelStarting = false;
    }

    public void unselectCurrentSelected()
    {
        if (selectedLevelData == null) return;
        selectedLevelData.gameObject.GetComponent<Image>().color = unselectedButtonColor;
        SceneManager.UnloadSceneAsync(selectedLevelData.LevelSceneName);
        InfoBackground.SetActive(false);
        selectedLevelData = null;
        displayObjects.SetActive(true);
    }

    public void SelectNewData(Leveldata levelData)
    {
        if (levelData == selectedLevelData)
        {
            unselectCurrentSelected();
        }
        else
        {
            unselectCurrentSelected();
            displayObjects.SetActive(false);
            SceneManager.LoadScene(levelData.LevelSceneName, LoadSceneMode.Additive);
            levelData.gameObject.GetComponent<Image>().color = selectedButtonColor;
            nameText.text = levelData.LevelName;
            descriptionText.text = levelData.LevelDescription;
            InfoBackground.SetActive(true);
            selectedLevelData = levelData;
        }
    }

    void LoadNewLevel()
    {
        SceneManager.LoadScene(selectedLevelData.LevelSceneName);
    }

    public void StartSelectedLevel()
    {
        if (selectedLevelData == null || LevelStarting) return;
        LevelStarting = true;
        DarkTransition.inst.BlackAppear(LoadNewLevel);
    }
}
