using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LevelTagsEnum
{
    test,
    test2    
}

public class Leveldata : MonoBehaviour
{
    public string LevelName;
    public string LevelDescription;
    public List<LevelTagsEnum> LevelTags;
    public string LevelSceneName;

    public void SendDataToManager()
    {
        LevelSelectionManager.inst.SelectNewData(this);
    }
}
