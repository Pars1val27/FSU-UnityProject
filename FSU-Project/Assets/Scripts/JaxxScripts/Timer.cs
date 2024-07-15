using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Clock : MonoBehaviour
{
    Clock timeInstance;
    [SerializeField] int startingTime;
    [SerializeField] public TMP_Text minText;
    [SerializeField] public TMP_Text secText;

    public float remainingTime;
    float secRemaining;
    float minRemaining;
    public bool timerRunning;
    // Start is called before the first frame update
    void Start()
    {
        timeInstance = this;
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
        minRemaining = ((int)remainingTime / 60);
        secRemaining = ((int)remainingTime % 60);
        minText.text = minRemaining.ToString("00");
        secText.text = secRemaining.ToString("00");
       
    }
}
