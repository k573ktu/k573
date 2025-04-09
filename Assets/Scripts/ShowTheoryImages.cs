using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowTheoryImages : MonoBehaviour
{
    public GameObject imagePrefab;
    public Transform contentPanel;
    public GameObject mainUI;
    public GameObject theoryUI;
    public ScrollRect scrollRect;

    public void LoadImagesFromFolder(string folderPath)
    {

        mainUI.SetActive(false);
        theoryUI.SetActive(true);

        // Reset scroll position to top
        scrollRect.verticalNormalizedPosition = 1f;

        // Clear previous images
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        // Load images...
        int i = 1;
        while (true)
        {
            string path = $"{folderPath}/page_{i}";
            Sprite sprite = Resources.Load<Sprite>(path);

            if (sprite == null)
            {
                break;
            }
            GameObject imageGO = Instantiate(imagePrefab, contentPanel);
            Image uiImage = imageGO.GetComponent<Image>();
            uiImage.sprite = sprite;
            uiImage.SetNativeSize();

            i++;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && theoryUI.activeSelf)
        {
            theoryUI.SetActive(false);
            mainUI.SetActive(true);
        }
    }

}

