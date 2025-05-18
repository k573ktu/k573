//using Firebase.Firestore;
//using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{ }/*
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private TextMeshProUGUI errorText;

    private FirebaseFirestore db;

    void Start()
    {
        // Initialize Firestore
        db = FirebaseFirestore.DefaultInstance;
        Debug.Log("Firestore initialized");

        password.contentType = TMP_InputField.ContentType.Password;
        password.ForceLabelUpdate();
    }

    public void TryLogin()
    {
        string code = username.text.Trim();
        string pwd = password.text.Trim();

        if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(pwd))
        {
            ShowError("Prašome įvesti kodą ir slaptažodį.");
            return;
        }

        Debug.Log($"Attempting login for code: {code}");

        // Query Firestore for matching code and password
        db.Collection("users")
            .WhereEqualTo("code", code)
            .WhereEqualTo("password", pwd)
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    ShowError("Serverio klaida, bandykite vėliau");
                    Debug.LogError("Error getting documents: " + task.Exception);
                    return;
                }

                var snapshot = task.Result;
                Debug.Log($"Found {snapshot.Count} matching users");

                if (snapshot.Count > 0)
                {
                    foreach (var document in snapshot.Documents)
                    {
                        string userType = document.GetValue<string>("type");
                        string userName = document.GetValue<string>("name");
                        string userCode = document.GetValue<string>("code");
                        string schoolName = userType == "teacher" ? document.GetValue<string>("schoolName") : "";

                        Debug.Log($"Login successful for user: {userName} ({userType})");
                        Success(userType, userName, userCode, schoolName);
                        return;
                    }
                }
                else
                {
                    ShowError("Neteisingas prisijungimo kodas arba slaptažodis");
                }
            });
    }

    void ShowError(string message)
    {
        errorText.text = message;
    }

    void Success(string userType, string userName, string userCode, string schoolName)
    {
        errorText.text = "";

        // Save the UserType, UserName, UserCode, and TeacherSchool to PlayerPrefs
        PlayerPrefs.SetString("UserType", userType);
        PlayerPrefs.SetString("UserName", userName);
        PlayerPrefs.SetString("UserCode", userCode);

        // Save the school name only for teachers
        if (userType == "teacher" && !string.IsNullOrEmpty(schoolName))
        {
            PlayerPrefs.SetString("TeacherSchool", schoolName);
            Debug.Log($"TeacherSchool saved: {schoolName}");
        }

        PlayerPrefs.Save();

        Debug.Log($"UserType saved: {userType}, UserName saved: {userName}, UserCode saved: {userCode}");

        // Load the main scene
        SceneManager.LoadScene("MainScene");
    }
}
*/