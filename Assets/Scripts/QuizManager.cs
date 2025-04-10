using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject titlePanel;
    public GameObject quizPanel;
    public GameObject resultsPanel;

    [Header("UI Elements")]
    public TextMeshProUGUI questionText;
    public Transform answerButtonContainer;
    public GameObject answerButtonPrefab;

    [Header("Results UI")]
    public GameObject reviewEntryPrefab;
    public Transform reviewContentParent;

    [Header("Quiz Data")]
    public TextAsset quizJsonFile;

    private QuizQuestionList quizData;
    private int currentQuestionIndex = 0;
    private int[] selectedAnswers;
    private bool quizFinished = false;
    private bool isViewingResults = false;

    public Button nextButton;
    public Button prevButton;
    public Button finishButton;

    private QuizQuestion[] generatedQuestions;

    void UpdateNavigationButtons()
    {
        prevButton.interactable = currentQuestionIndex > 0;
        nextButton.interactable = currentQuestionIndex < quizData.questions.Length - 1;
    }

    void CheckIfCanFinish()
    {
        foreach (var answer in selectedAnswers)
        {
            if (answer == -1)
            {
                finishButton.interactable = false;
                return;
            }
        }

        finishButton.interactable = true;
    }

    public void StartQuizWithJson(TextAsset jsonFile)
    {
        quizJsonFile = jsonFile;
        StartQuiz();
    }

    public void StartQuiz()
    {
        titlePanel.SetActive(false);
        quizPanel.SetActive(true);
        resultsPanel.SetActive(false);
        quizFinished = false;

        LoadQuiz();
        SelectRandomQuestions(10);

        selectedAnswers = new int[quizData.questions.Length];
        generatedQuestions = new QuizQuestion[quizData.questions.Length];

        for (int i = 0; i < selectedAnswers.Length; i++)
        {
            selectedAnswers[i] = -1;

            var q = quizData.questions[i];
            generatedQuestions[i] = q.dynamic ? GenerateDynamicQuestion(q) : q;
        }

        currentQuestionIndex = 0;
        ShowQuestion(currentQuestionIndex);
        UpdateNavigationButtons();
        CheckIfCanFinish();
    }
    void SelectRandomQuestions(int count)
    {
        List<QuizQuestion> originalList = new List<QuizQuestion>(quizData.questions);
        System.Random rand = new System.Random();

        List<QuizQuestion> selected = new List<QuizQuestion>();

        for (int i = 0; i < count && originalList.Count > 0; i++)
        {
            int index = rand.Next(originalList.Count);
            selected.Add(originalList[index]);
            originalList.RemoveAt(index);
        }

        quizData.questions = selected.ToArray();
    }


    void LoadQuiz()
    {
        string jsonWrapped = "{\"questions\":" + quizJsonFile.text + "}";
        quizData = JsonUtility.FromJson<QuizQuestionList>(jsonWrapped);
    }

    void ShowQuestion(int index)
    {
        QuizQuestion q = generatedQuestions[index];

        questionText.text = $"{index + 1}. {q.question}";

        foreach (Transform child in answerButtonContainer)
            Destroy(child.gameObject);

        for (int i = 0; i < q.answers.Length; i++)
        {
            GameObject btn = Instantiate(answerButtonPrefab, answerButtonContainer);
            var text = btn.GetComponentInChildren<TextMeshProUGUI>();
            text.text = q.answers[i];

            int answerIndex = i;
            Button button = btn.GetComponent<Button>();

            text.color = selectedAnswers[index] == i ? Color.yellow : Color.white;

            button.onClick.AddListener(() =>
            {
                selectedAnswers[index] = answerIndex;
                ShowQuestion(index);
                CheckIfCanFinish();
            });
        }
    }

    QuizQuestion GenerateDynamicQuestion(QuizQuestion original)
    {
        System.Random rand = new System.Random(); // only declare this once per method

        Dictionary<string, double> vars = new Dictionary<string, double>();

        vars["a"] = rand.NextDouble() * (100 - 1) + 1;       // 1 to 100
        vars["m"] = rand.NextDouble() * (100 - 1) + 1;
        vars["g"] = (double)9.81;                                     // constant, double by default
        vars["f"] = rand.NextDouble() * (200 - 20) + 1;      // 20 to 200
        vars["f1"] = rand.NextDouble() * (200 - 10) + 1;      // 10 to 50
        vars["f2"] = rand.NextDouble() * (200 - 10) + 1;      // 10 to 40
        vars["f3"] = rand.NextDouble() * (200 - 10) + 1;      // 10 to 30
        vars["m1"] = rand.NextDouble() * (200 - 5) + 1;         // 5 to 8
        vars["m2"] = rand.NextDouble() * (200 - 7) + 1;         // 7 to 9
        vars["r"] = rand.NextDouble() * (200 - 1) + 1;          // fixed at 1 (this one doesn't vary)
        vars["G"] = (double)6.674;                                // gravitational constant



        vars["m*a"] = vars["m"] * vars["a"];
        vars["m/a"] = vars["m"] / vars["a"];
        vars["a/m"] = vars["a"] / vars["m"];
        vars["m+a"] = vars["m"] + vars["a"];
        vars["m-a"] = vars["m"] - vars["a"];

        vars["m*g"] = vars["m"] * vars["g"];
        vars["m/g"] = vars["m"] / vars["g"];
        vars["g/m"] = vars["g"] / vars["m"];
        vars["m*10"] = vars["m"] * 10;

        vars["f/a"] = vars["f"] / vars["a"];
        vars["a/f"] = vars["a"] / vars["f"];
        vars["a*f"] = vars["a"] * vars["f"];
        vars["f+a"] = vars["f"] + vars["a"];

        vars["fSum"] = vars["f1"] + vars["f2"] + vars["f3"];
        vars["fDif"] = vars["f1"] - vars["f2"] - vars["f3"];
        vars["f1+f2-f3"] = vars["f1"] + vars["f2"] - vars["f3"];
        vars["f1-f2+f3"] = vars["f1"] - vars["f2"] + vars["f3"];

        vars["G*(m1*m2/r^2)"] = (vars["G"] * vars["m1"] * vars["m2"]) / Math.Pow(vars["r"], 2);
        vars["G*(m1+m2/r)"] = (vars["G"] * (vars["m1"] + vars["m2"])) / vars["r"];
        vars["(m1+m2/r)"] = (vars["m1"] + vars["m2"]) / vars["r"];
        vars["G*(m1+m2/r^2)"] = (vars["G"] * (vars["m1"] + vars["m2"])) / Math.Pow(vars["r"], 2);

        vars["sqrt((G*m1*m2)/f)"] = Math.Sqrt((vars["G"] * vars["m1"] * vars["m2"]) / vars["f"]);
        vars["(G*m1*m2)/f"] = (vars["G"] * vars["m1"] * vars["m2"]) / vars["f"];
        vars["(G+m1+m2)/f"] = (vars["G"] + vars["m1"] + vars["m2"]) / vars["f"];
        vars["G+m1+m2"] = vars["G"] + vars["m1"] + vars["m2"];

        vars["f*r^2 / G * m1"] = (vars["f"] * Math.Pow(vars["r"], 2)) / vars["G"] * vars["m1"];
        vars["f*r/G*m1"] = (vars["f"] * vars["r"]) / vars["G"] * vars["m1"];
        vars["f*r^2/m1"] = (vars["f"] * Math.Pow(vars["r"], 2)) / vars["m1"];
        vars["r^2/m1*G"] = (Math.Pow(vars["r"], 2)) / vars["G"] * vars["m1"];


        string newQuestion = ReplacePlaceholders(original.question, vars);
        string[] newAnswers = new string[original.answers.Length];
        for (int i = 0; i < newAnswers.Length; i++)
            newAnswers[i] = ReplacePlaceholders(original.answers[i], vars);

        return new QuizQuestion
        {
            question = newQuestion,
            answers = newAnswers,
            correctIndex = original.correctIndex,
            dynamic = false
        };
    }

    string ReplacePlaceholders(string input, Dictionary<string, double> vars)
    {
        foreach (var pair in vars)
            input = input.Replace("{" + pair.Key + "}", pair.Value.ToString("0.##"));

        return input;
    }

    void ShowResults()
    {
        quizPanel.SetActive(false);
        resultsPanel.SetActive(true);
        isViewingResults = true;

        foreach (Transform child in reviewContentParent)
            Destroy(child.gameObject);

        for (int i = 0; i < quizData.questions.Length; i++)
        {
            var question = generatedQuestions[i];
            int selected = selectedAnswers[i];

            GameObject entry = Instantiate(reviewEntryPrefab, reviewContentParent);
            TextMeshProUGUI[] textFields = entry.GetComponentsInChildren<TextMeshProUGUI>();
            textFields[0].text = $"{i + 1}. {question.question}";

            for (int j = 1; j < textFields.Length; j++)
                Destroy(textFields[j].gameObject);

            for (int j = 0; j < question.answers.Length; j++)
            {
                GameObject answerLine = Instantiate(textFields[0].gameObject, entry.transform);
                var text = answerLine.GetComponent<TextMeshProUGUI>();
                text.text = question.answers[j];

                if (j == question.correctIndex)
                    text.color = Color.green;
                else if (j == selected)
                    text.color = Color.red;
                else
                    text.color = Color.white;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && titlePanel.transform.parent.gameObject.activeSelf)
        {
            if (isViewingResults)
            {
                resultsPanel.SetActive(false);
                titlePanel.SetActive(true);
                selectedAnswers = null;
                currentQuestionIndex = 0;
                isViewingResults = false;
            }
            else if (quizPanel.activeSelf)
            {
                ReturnToTitle();
            }
            else if (titlePanel.activeSelf)
            {
                ReturnToTitle();
                UiManager.inst.GoMain();
            }
        }
    }

    public void GoToNextQuestion()
    {
        if (currentQuestionIndex < quizData.questions.Length - 1)
        {
            currentQuestionIndex++;
            ShowQuestion(currentQuestionIndex);
            UpdateNavigationButtons();
        }
    }

    public void GoToPreviousQuestion()
    {
        if (currentQuestionIndex > 0)
        {
            currentQuestionIndex--;
            ShowQuestion(currentQuestionIndex);
            UpdateNavigationButtons();
        }
    }

    public void FinishQuiz()
    {
        if (quizFinished) return;

        quizFinished = true;
        ShowResults();
    }

    [System.Serializable]
    public class QuizQuestion
    {
        public string question;
        public string[] answers;
        public int correctIndex;
        public bool dynamic;
    }

    [System.Serializable]
    public class QuizQuestionList
    {
        public QuizQuestion[] questions;
    }
    public void ReturnToTitle()
    {
        resultsPanel.SetActive(false);
        quizPanel.SetActive(false);
        titlePanel.SetActive(true);

        selectedAnswers = null;
        currentQuestionIndex = 0;
        isViewingResults = false;
        quizFinished = false;
    }

}
