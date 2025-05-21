using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private TextMeshProUGUI errorText;
    //[SerializeField] private GameObject loadingIndicator;
    // Hardcoded user entries
    private readonly User[] users = new User[]
    {
        new User("username", "password", "student", "Test Student", ""),
        new User("mokinys", "mokinys", "student", "Test Student2", "KTU"),
        new User("mokytojas", "mokytojas", "teacher", "Kestutis Jankauskas", "KTU")
        // add more users as needed
        // new User("code", "password", "type", "name", "schoolName")
        // example teacher:
        // new User("teacher1", "teach123", "teacher", "John Doe", "Example School")
    };
    void Start()
    {
        password.contentType = TMP_InputField.ContentType.Password;
        password.ForceLabelUpdate();
        //loadingIndicator.SetActive(false);
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

        //loadingIndicator.SetActive(true);
        errorText.text = "";

        // First try Firebase authentication
        TryFirebaseLogin(code, pwd, (success, userType, userName, schoolName) =>
        {
            if (success)
            {
                Debug.Log($"Firebase login successful for {userName}");
                Success(userType, userName, code, schoolName);
            }
            else
            {
                // Fallback to hardcoded users
                TryLocalLogin(code, pwd);
            }
            //loadingIndicator.SetActive(false);
        });
    }

    private void TryFirebaseLogin(string code, string pwd, Action<bool, string, string, string> callback)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        // Call JavaScript Firebase authentication
        FirebaseLogin(code, pwd, 
            (userJson) => {
                var user = JsonUtility.FromJson<FirebaseUser>(userJson);
                callback(true, user.type, user.name, user.schoolName);
            },
            (error) => {
                Debug.LogWarning($"Firebase login failed: {error}");
                callback(false, null, null, null);
            });
#else
        // Simulate failure in editor/standalone to test fallback
        Debug.Log("Firebase login would be attempted in WebGL build");
        callback(false, null, null, null);
#endif
    }

    private void TryLocalLogin(string code, string pwd)
    {
        foreach (var user in users)
        {
            if (user.code == code && user.password == pwd)
            {
                Debug.Log($"Local login successful for user: {user.name} ({user.type})");
                Success(user.type, user.name, user.code, user.schoolName);
                return;
            }
        }
        ShowError("Neteisingas prisijungimo kodas arba slaptažodis");
    }

    void ShowError(string message)
    {
        errorText.text = message;
    }

    void Success(string userType, string userName, string userCode, string schoolName)
    {
        errorText.text = "";

        // save UserType, UserName, UserCode, and TeacherSchool to PlayerPrefs
        PlayerPrefs.SetString("UserType", userType);
        PlayerPrefs.SetString("UserName", userName);
        PlayerPrefs.SetString("UserCode", userCode);
        // save school name
        if (userType == "teacher" && !string.IsNullOrEmpty(schoolName))
        {
            PlayerPrefs.SetString("TeacherSchool", schoolName);
            Debug.Log($"TeacherSchool saved: {schoolName}");
        }
        PlayerPrefs.Save();
        Debug.Log($"UserType saved: {userType}, UserName saved: {userName}, UserCode saved: {userCode}");
        SceneManager.LoadScene("MainScene");
    }

    // Helper class to store user data
    private class User
    {
        public string code;
        public string password;
        public string type;
        public string name;
        public string schoolName;

        public User(string code, string password, string type, string name, string schoolName)
        {
            this.code = code;
            this.password = password;
            this.type = type;
            this.name = name;
            this.schoolName = schoolName;
        }
    }
#if UNITY_WEBGL && !UNITY_EDITOR
    [System.Serializable]
    private class FirebaseUser
    {
        public string type;
        public string name;
        public string schoolName;
    }

    [DllImport("__Internal")]
    private static extern void FirebaseLogin(string username, string password, 
        Action<string> onSuccess, Action<string> onError);
#endif
}