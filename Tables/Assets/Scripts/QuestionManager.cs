using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;



public class QuestionManager : MonoBehaviour
{
    private class Question
    {
        public int FirstTerm { get; set; }
        public int SecondTerm { get; set; }
    }

    public TMP_Text firstTermText;
    public TMP_Text secondTermText;
    public TMP_Text givenAnswerText;

    public bool autoCommit;
    public bool sortQuestions;
    public bool enqueueWrongAnswers;

    private int firstTerm;
    private int secondTerm;
    private int answer;
    private int expectedAnswerLenght;

    public int[] tables;

    private Queue<Question> questionsQueue;

    // Start is called before the first frame update
    void Start()
    {
        var questions = new List<Question>(tables.Length * 10);

        foreach (var table in tables)
        {
            questions.AddRange(Enumerable.Range(1, 10).Select(x => new Question
            {
                FirstTerm = table,
                SecondTerm = x
            }));
        }

        if (sortQuestions)
        { 
            questions = questions.OrderBy(x => Guid.NewGuid()).ToList();
        }

        questionsQueue = new Queue<Question>(questions);
        SetupNewQuestion();
    }

    private void SetupNewQuestion()
    {
        var question = questionsQueue.Dequeue();
        firstTermText.text = question.FirstTerm.ToString();
        firstTerm = question.FirstTerm;

        secondTermText.text = question.SecondTerm.ToString();
        secondTerm = question.SecondTerm;

        givenAnswerText.text = string.Empty;
        answer = firstTerm * secondTerm;
        expectedAnswerLenght = answer.ToString().Length;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void CheckAndAssert()
    {
        if (ShouldAssertAnswer())
        {
            AssertAnswer();
        }
    }

    public void AssertAnswer()
    {
        if (answer == int.Parse(givenAnswerText.text))
        {

        }
        else
        {
            if (enqueueWrongAnswers)
            {
                questionsQueue.Enqueue(new Question { FirstTerm = firstTerm, SecondTerm = secondTerm });
            }
        }

        SetupNewQuestion();
    }

    public bool ShouldAssertAnswer()
    {
        return  autoCommit && expectedAnswerLenght == givenAnswerText.text.Length;
    }
}
