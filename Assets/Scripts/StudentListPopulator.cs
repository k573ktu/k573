using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
//using Firebase.Firestore;
using System.Linq;

public class StudentListPopulator : MonoBehaviour
{ }/*
    public Transform contentParent;
    public Font textFont;
    public int fontSize = 18;
    public Color textColor = Color.black;
    public Color headerColor = Color.white;
    public int rowHeight = 40;
    public int columnWidth = 150;
    public Color lowScoreColor = Color.red;
    public Color mediumScoreColor = Color.yellow;
    public Color highScoreColor = Color.green;

    // To keep the same random values for each student
    private Dictionary<string, (int jegos, int dangus)> studentStats = new Dictionary<string, (int, int)>();

    void Start()
    {
        CreateHeaderRow();
        FetchStudentsFromDatabase();
    }

    private void CreateHeaderRow()
    {
        GameObject headerRow = CreateRow();
        AddTextCell(headerRow.transform, "Vardas", headerColor);
        AddTextCell(headerRow.transform, "Pavardė", headerColor);
        AddTextCell(headerRow.transform, "Klasė", headerColor);
        AddTextCell(headerRow.transform, "Jėgos (%)", headerColor);
        AddTextCell(headerRow.transform, "Dangaus kūnai (%)", headerColor);
    }

    private async void FetchStudentsFromDatabase()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        // Fetch all students from the database
        QuerySnapshot snapshot = await db.Collection("users")
                                         .WhereEqualTo("type", "student")
                                         .GetSnapshotAsync();

        if (!snapshot.Documents.Any())
        {
            Debug.LogWarning("No students found in the database.");
            return;
        }

        foreach (DocumentSnapshot document in snapshot.Documents)
        {
            string name = document.GetValue<string>("name");
            string surname = document.GetValue<string>("surname");
            string className = document.GetValue<string>("className");
            string studentKey = $"{name}_{surname}";

            // Generate or reuse random stats for each student
            if (!studentStats.ContainsKey(studentKey))
            {
                int jegos = Random.Range(10, 101);
                int dangus = Random.Range(10, 101);
                studentStats[studentKey] = (jegos, dangus);
            }

            (int jegos, int dangus) stats = studentStats[studentKey];

            // Create the student row
            GameObject studentRow = CreateRow();
            AddTextCell(studentRow.transform, name, textColor);
            AddTextCell(studentRow.transform, surname, textColor);
            AddTextCell(studentRow.transform, className, textColor);

            // Add Jëgos with color
            Color jegosColor = GetScoreColor(stats.jegos);
            AddTextCell(studentRow.transform, $"{stats.jegos}%", jegosColor);

            // Add Dangaus kûnai with color
            Color dangusColor = GetScoreColor(stats.dangus);
            AddTextCell(studentRow.transform, $"{stats.dangus}%", dangusColor);
        }

        Debug.Log("Students loaded successfully.");
    }

    private Color GetScoreColor(int score)
    {
        if (score <= 40)
            return lowScoreColor;
        else if (score <= 70)
            return mediumScoreColor;
        else
            return highScoreColor;
    }

    private GameObject CreateRow()
    {
        GameObject row = new GameObject("Row", typeof(RectTransform));
        row.transform.SetParent(contentParent, false);

        HorizontalLayoutGroup layoutGroup = row.AddComponent<HorizontalLayoutGroup>();
        layoutGroup.childControlWidth = false;
        layoutGroup.childForceExpandWidth = false;
        layoutGroup.childAlignment = TextAnchor.MiddleLeft;
        layoutGroup.spacing = 10;
        layoutGroup.padding = new RectOffset(5, 5, 5, 5);

        return row;
    }

    private void AddTextCell(Transform parent, string text, Color color)
    {
        // Create the text object
        GameObject textObject = new GameObject("Cell", typeof(Text));
        textObject.transform.SetParent(parent, false);

        // Configure the Text component
        Text cellText = textObject.GetComponent<Text>();
        cellText.font = textFont;
        cellText.fontSize = fontSize;
        cellText.color = color;
        cellText.alignment = TextAnchor.MiddleLeft;
        cellText.text = text;

        // Add a LayoutElement to enforce width
        LayoutElement layoutElement = textObject.AddComponent<LayoutElement>();
        layoutElement.preferredWidth = columnWidth;
        layoutElement.minWidth = columnWidth;
        layoutElement.flexibleWidth = 0;

        // Ensure the RectTransform has no unwanted padding
        RectTransform rectTransform = textObject.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.pivot = new Vector2(0, 1);
        rectTransform.sizeDelta = new Vector2(columnWidth, rowHeight);
        rectTransform.anchoredPosition = Vector2.zero;
    }
}
    */