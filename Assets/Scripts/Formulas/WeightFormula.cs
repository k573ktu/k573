using System;
using UnityEngine;

public class WeightFormula : Formula
{
    [SerializeField] GameObject analyzedObject;
    protected override void FormulaToText()
    {
        float m = OptionsManager.inst.getValue("Kamuoliuko masė");
        double a = analyzedObject.GetComponent<Rigidbody2D>().linearVelocity.magnitude;
        double F = m * a;
        formulaText.text = string.Format("{0:F"+decimals+"} = {1}*{2:F"+decimals+"}", F, m, a);
    }
}
