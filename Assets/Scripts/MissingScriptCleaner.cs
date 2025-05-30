using UnityEditor;
using UnityEngine;

public class MissingScriptCleaner
{/*

    [MenuItem("Tools/Clean Missing Scripts In Scene")]
    public static void CleanMissingScripts()
    {
        GameObject[] allObjects = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        int count = 0;

        foreach (GameObject go in allObjects)
        {
            int removed = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
            if (removed > 0)
            {
                count += removed;
                Debug.Log($"Removed {removed} missing script(s) from: {go.name}");
            }
        }

        Debug.Log($"Done. Removed {count} missing scripts in total.");
    }
    */
}
