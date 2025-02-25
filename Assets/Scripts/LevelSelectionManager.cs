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

    bool LevelStarting;

    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        LevelStarting = false;
    }

    void unselectCurrentSelected()
    {
        if (selectedLevelData == null) return;
        selectedLevelData.gameObject.GetComponent<Image>().color = unselectedButtonColor;
        InfoBackground.SetActive(false);
        selectedLevelData = null;
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
            levelData.gameObject.GetComponent<Image>().color = selectedButtonColor;
            nameText.text = levelData.LevelName;
            descriptionText.text = levelData.LevelDescription;
            InfoBackground.SetActive(true);
            selectedLevelData = levelData;
        }
    }

    public void StartSelectedLevel()
    {
        if (selectedLevelData == null || LevelStarting) return;
        LevelStarting = true;
        SceneManager.LoadScene(selectedLevelData.LevelSceneName);
    }
}
