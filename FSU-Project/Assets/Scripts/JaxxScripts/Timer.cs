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
            if(remainingTime <= 0)
            {
                timerRunning = false;

                Debug.Log("Timer End");
            }
        }
    }

    public void StartTimer()
    {
        timerRunning = true;
        Debug.Log("Timer Start");
    }

    public void StopTimer()
    {
        timerRunning = false;
        Debug.Log("Timer Stop");
    }

    public void EditTIme(int seconds)
    {
        remainingTime += seconds;
        Debug.Log("Time Changed");
    }

    public void UpdateTimer()
    {
        minRemaining = (remainingTime / 60);
        secRemaining = (remainingTime % 60);
        minText.text = minRemaining.ToString("f0");
        secText.text = secRemaining.ToString("f0");
       
    }
}
