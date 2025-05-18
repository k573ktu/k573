using UnityEngine;
using TMPro;
//using Firebase.Firestore;
//using Firebase.Extensions;

public class TeacherPanelManager : MonoBehaviour
{
}
    /*
    [SerializeField] private TextMeshProUGUI teacherInfoText;

    private FirebaseFirestore db;

    private void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        LoadTeacherInfo();
    }

    private void LoadTeacherInfo()
    {
        string userCode = PlayerPrefs.GetString("UserCode", "unknown");

        if (userCode == "unknown")
        {
            teacherInfoText.text = "Error: No user code found.";
            Debug.LogError("No user code found in PlayerPrefs.");
            return;
        }

        // Retrieve teacher info from Firestore
        db.Collection("users")
            .WhereEqualTo("code", userCode)
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    teacherInfoText.text = "Error loading data.";
                    Debug.LogError("Error getting teacher data: " + task.Exception);
                    return;
                }

                var snapshot = task.Result;
                if (snapshot.Count > 0)
                {
                    foreach (var document in snapshot.Documents)
                    {
                        string name = document.GetValue<string>("name");
                        string surname = document.GetValue<string>("surname");
                        string schoolName = document.GetValue<string>("schoolName");

                        teacherInfoText.text = $"{name} {surname}\n{schoolName}";
                        Debug.Log($"Teacher Info Loaded: {name} {surname} - {schoolName}");
                        return;
                    }
                }
                else
                {
                    teacherInfoText.text = "No teacher data found.";
                    Debug.LogWarning("No teacher data found for the given code.");
                }
            });
    }
}
    */