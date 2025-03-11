using UnityEngine;
using UnityEngine.UI;

public class ScriptToggleOptionData : MonoBehaviour
{
    [SerializeField] MonoBehaviour firstScript;
    [SerializeField] MonoBehaviour secondScript;

    [SerializeField] Toggle toggle;

    private void Start()
    {
        if (toggle.isOn)
        {
            secondScript.enabled = true;
            firstScript.enabled = false;
        }
        else
        {
            secondScript.enabled = false;
            firstScript.enabled = true;
        }
    }

    public void OnUpdate()
    {
        if (toggle.isOn)
        {
            secondScript.enabled = true;
            firstScript.enabled = false;
        }
        else
        {
            secondScript.enabled = false;
            firstScript.enabled = true;
        }
    }
}
