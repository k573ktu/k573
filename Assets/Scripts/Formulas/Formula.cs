using TMPro;
using UnityEngine;

public class Formula : MonoBehaviour
{
    [SerializeField] protected int decimals;
    [SerializeField] protected TextMeshProUGUI formulaText;
    bool calculating;

    private void Start()
    {
        calculating = false;
        formulaText.text = "";
        FormulaManager.inst.InsertFormula(this);
    }

    public void StartCalculating()
    {
        calculating = true;
    }

    public void StopCalculating()
    {
        calculating = false;
        formulaText.text = "";
    }

    protected virtual void FormulaToText() { }

    private void Update()
    {
        if (!calculating) return;
        FormulaToText();
    }
}
