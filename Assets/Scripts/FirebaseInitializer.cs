//using Firebase;
//using Firebase.Extensions;
using UnityEngine;

public class FirebaseInitializer : MonoBehaviour
{ }
    /*
    void Start()
    {
        Debug.Log("Attempting to initialize Firebase...");

        // Check Firebase dependencies
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                Debug.Log("Firebase initialized successfully");
                Debug.Log("Project ID: " + FirebaseApp.DefaultInstance.Options.ProjectId);
                Debug.Log("App ID: " + FirebaseApp.DefaultInstance.Options.AppId);
                Debug.Log("API Key: " + FirebaseApp.DefaultInstance.Options.ApiKey);
            }
            else
            {
                Debug.LogError("Firebase initialization failed: " + task.Result);
            }
        });
    }
}
    */