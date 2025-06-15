public class Task
{
    public string Description { get; private set; }
    public bool IsComplete { get; private set; }

    private System.Func<bool> checkCondition;

    public Task(string description, System.Func<bool> condition)
    {
        Description = description;
        checkCondition = condition;
        IsComplete = false;
    }

    public void UpdateTask()
    {
        if (!IsComplete && checkCondition())
        {
            IsComplete = true;
        }
    }

    public string GetStatusText()
    {
        return $"{Description} - {(IsComplete ? "<color=green>[v]</color>" : "<color=red>[x]</color>")}";
    }
}