using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public static OptionsManager inst;

    Dictionary<string, float> dict;

    private void Awake()
    {
        if (inst == null) inst = this;
    }

    private void Start()
    {
        dict = new Dictionary<string, float>();
    }

    public void InsertOption(string key, float value)
    {
        dict[key] = value;
    }

    public float getValue(string key)
    {
        float res;
        if (dict.TryGetValue(key, out res))
        {
            return res;
        }
        else
        {
            return 0;
        }
    }
}
