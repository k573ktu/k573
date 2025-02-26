using System.Collections.Generic;
using UnityEngine;

public class FormulaManager : MonoBehaviour
{
    public static FormulaManager inst;

    List<Formula> formulas;

    private void Awake()
    {
        if (inst == null) inst = this;
        formulas = new List<Formula>();
    }

    public void InsertFormula(Formula formula)
    {
        formulas.Add(formula);
    }

    public void StartAllFormulas()
    {
        foreach(var i in formulas)
        {
            i.StartCalculating();
        }
    }

    public void StopAllFormulas()
    {
        foreach (var i in formulas)
        {
            i.StopCalculating();
        }
    }
}
