using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private TextMeshProUGUI errorText;

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
        // check against hardcoded users, works for webGL
        foreach (var user in users)
        {
            if (user.code == code && user.password == pwd)
            {
                Debug.Log($"Login successful for user: {user.name} ({user.type})");
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
}