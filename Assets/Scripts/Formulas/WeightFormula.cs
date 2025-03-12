using System;
using UnityEngine;

public class WeightFormula : Formula
{
    [SerializeField] GameObject analyzedObject;
    protected override void FormulaToText()
    {
        float m = OptionsManager.inst.getValue("Transporto priemonės masė");
        double a = Math.Round(analyzedObject.GetComponent<Rigidbody2D>().linearVelocity.magnitude,decimals);
        double F = Math.Round(m * a,decimals);
        formulaText.text = string.Format("{0:F"+decimals+"} = {1}*{2:F"+decimals+"}", F, m, a);
    }
}
