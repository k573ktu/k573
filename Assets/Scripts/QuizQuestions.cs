[System.Serializable]
public class QuizQuestion
{
    public string question;
    public string[] answers;
    public int correctIndex;
}

[System.Serializable]
public class QuizQuestionList
{
    public QuizQuestion[] questions;
}

