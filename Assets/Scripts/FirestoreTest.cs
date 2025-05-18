//using Firebase.Firestore;
//using Firebase.Extensions;
using UnityEngine;

public class FirestoreTest : MonoBehaviour
{ }/*
    void Start()
    {
        Debug.Log("Attempting to connect to Firestore...");

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        // Test Firestore connection by fetching a simple document
        db.Collection("test").Document("connectionTest").GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Firestore connection failed: " + task.Exception);
            }
            else
            {
                Debug.Log("Firestore connection successful");
                Debug.Log("Document data: " + task.Result.ToDictionary());
            }
        });
    }
}
*/