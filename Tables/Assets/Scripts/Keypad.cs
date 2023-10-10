using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    public TMP_Text visibleAnswer;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button button6;
    public Button button7;
    public Button button8;
    public Button button9;
    public Button button0;

    public void EnterNumber(Button theButton)
    {
        visibleAnswer.text += theButton.GetComponentInChildren<TMP_Text>().text;

        Component.FindAnyObjectByType<QuestionManager>().CheckAndAssert();
    }
}
