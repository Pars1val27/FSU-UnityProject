using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{

    [SerializeField] int startingTime;
    [SerializeField] public TMP_Text minText;
    [SerializeField] public TMP_Text secText;

    float remainingTime;
    float secRemaining;
    float minRemaining;
    bool timerRunning;
    // Start is called before the first frame update
    void Start()
    {
        remainingTime = startingTime;
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if(timerRunning)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimer();
        }
    }

    public void StartTimer()
    {
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void EditTIme(int seconds)
    {
        remainingTime += seconds;
    }

    public void UpdateTimer()
    {
        minRemaining = (remainingTime / 60);
        secRemaining = (remainingTime % 60);
        minText.text = minRemaining.ToString("f0");
        secText.text = secRemaining.ToString("f0");
       
    }
}
