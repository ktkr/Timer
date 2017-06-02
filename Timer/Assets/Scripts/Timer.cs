using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public Text timeNumber;
    public GameObject softBell;
    public GameObject buzzer;
    public InputField inp;

    private bool counting = false;
    private bool paused = false;
    private float time;
    private float timeLeft;

    private void Start()
    {
        DisplayCurrentTime();
    }

    private string ConvertToUnits(float timeGiven)
    {
        
        string seconds = Mathf.Floor((timeGiven % 60)).ToString("00");
        string minutes = Mathf.Floor((timeGiven / 60 % 60)).ToString("00");
        string hours = Mathf.Floor((timeGiven / 60 / 60 % 24)).ToString("00");
        string days = Mathf.Floor((timeGiven / 60 / 60 / 24)).ToString();
        return days + "d : " + hours + "h : " + minutes + "m : " + seconds + " s";
    }

    IEnumerator StartCountdownTimer(float timeToCount)
    {
        if (timeToCount < 0)
        {
            yield break;
        }
        counting = true;
        while (counting)
        {
            if (timeToCount <= 0)
            {
                counting = false;
                PlayBuzzer();
                timeNumber.text = "TIME'S UP!";
            }

            else if(Mathf.Round(timeToCount) == 30)
            {
                PlaySoftWarning();
            }
            while (paused)
            {
                yield return null;
            }
            timeNumber.text = ConvertToUnits(timeToCount);
            timeToCount--;
            yield return new WaitForSeconds(1);
        }
    }

    public void StartTimer() 
    {
        StopAllCoroutines();
        paused = false;
        try
        {
            StartCoroutine(StartCountdownTimer(float.Parse(inp.text)));
        }
        catch (System.Exception)
        {
            timeNumber.text = "Incorrect input!";
        }
    }

    public void DisplayCurrentTime()
    {
        StopAllCoroutines();
        StartCoroutine(CurrentTime());
    }

    IEnumerator CurrentTime()
    {
        for (;;)
        {
            timeNumber.text = "Time now is " + System.DateTime.Now.ToString("h:mm:ss tt");
            yield return new WaitForSeconds(1);
        }
    }

    public void TogglePauseCountdownTimer()
    {
        paused = !paused;
    }

    public void RestartTimer()
    {
        StopAllCoroutines();
        paused = false;
        timeNumber.text = "Timer reset";
    }

    public void PlaySoftWarning()
    {
        Instantiate(softBell);
    }

    public void PlayBuzzer()
    {
        Instantiate(buzzer);
    }
}
