using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI errorText;

    [SerializeField] TMP_InputField username;
    [SerializeField] TMP_InputField password;


    void GiveError(string error)
    {
        errorText.text = error; 
    }

    void Success()
    {
        DarkTransition.inst.BlackAppear(() => { SceneManager.LoadScene("MainScene"); });
    }

    public void TryLogin()
    {
        if(username.text == "test" && password.text == "test") // your code here (also add error if can't connect to database)
        {
            Success();
        }
        else
        {
            GiveError("Neteisingas prisijungimo kodas arba slaptažodis");
        }
    }
}
