using System;
using UnityEngine;

public class WeightFormula : Formula
{
    [SerializeField] GameObject analyzedObject;
    protected override void FormulaToText()
    {
        if (analyzedObject == null)
        {
            analyzedObject = GameObject.FindGameObjectWithTag("main").transform.GetChild(0).gameObject;
        }
        float m = OptionsManager.inst.getValue("Transporto Priemonė");
        double a = Math.Round(analyzedObject.GetComponent<Rigidbody2D>().linearVelocity.magnitude,decimals);
        double F = Math.Round(m * a,decimals);
        formulaText.text = string.Format("{0:F"+decimals+"} = {1}*{2:F"+decimals+"}", F, m, a);
    }
}
