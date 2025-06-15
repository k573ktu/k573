using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptToggleOptionData : MonoBehaviour
{
    [SerializeField] List<MonoBehaviour> firstScript;
    [SerializeField] List<MonoBehaviour> secondScript;



    //[SerializeField] Toggle toggle;

    //bool check;
    //bool curr;

    //private void Start()
    //{
    //    check = toggle.isOn;

    //    curr = !check;
    //}

    public void OnUpdate()
    {
        //check = toggle.isOn;
    }

    private void Update()
    {

        //if (check != curr && GameManager.inst.simPlaying)
        //{
        //    curr = check;
        //    if (curr)
        //    {
        //        secondScript.ForEach((i) => i.enabled = true);
        //        firstScript.ForEach((i) => i.enabled = false);
        //    }
        //    else
        //    {
        //        secondScript.ForEach((i) => i.enabled = false);
        //        firstScript.ForEach((i) => i.enabled = true);
        //    }
        //}else if(curr && !GameManager.inst.simPlaying)
        //{
        //    curr = false;
        //    if (curr)
        //    {
        //        secondScript.ForEach((i) => i.enabled = true);
        //        firstScript.ForEach((i) => i.enabled = false);
        //    }
        //    else
        //    {
        //        secondScript.ForEach((i) => i.enabled = false);
        //        firstScript.ForEach((i) => i.enabled = true);
        //    }
        //}

        if (GameManager.inst.simPlaying)
        {
            secondScript.ForEach((i) => i.enabled = true);
            firstScript.ForEach((i) => i.enabled = false);
        }
        else
        {
            secondScript.ForEach((i) => i.enabled = false);
            firstScript.ForEach((i) => i.enabled = true);
        }
    }
}
