using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    private float timerStart;
    public TextMeshProUGUI timerText;

    void Start()
    {
        timerStart = 0;
        timerText.text = timerStart.ToString("F2");
    }

    void Update()
    {
        timerStart += Time.deltaTime;
        timerText.text = timerStart.ToString("F2");
    }
}
