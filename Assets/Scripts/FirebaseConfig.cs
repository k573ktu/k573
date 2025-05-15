using UnityEngine;

public class FirebaseConfig : MonoBehaviour
{
    public static FirebaseConfig instance;

    [Header("Firebase Configuration")]
    public string apiKey = "AIzaSyBY5viSrxrkVDkUdZajtaWr571OmlUzcOo";
    public string authDomain = "fizikon-3e447.firebaseapp.com";
    public string projectId = "fizikon-3e447";
    public string storageBucket = "fizikon-3e447.firebasestorage.app";
    public string messagingSenderId = "899140351328";
    public string appId = "1:899140351328:web:5ff3d279b0c22d061eda05";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("FirebaseConfig initialized successfully");
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
