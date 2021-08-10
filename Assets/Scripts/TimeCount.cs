using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeCount : MonoBehaviour
{
    private int seconds, minutes, hours, recordSeconds, recordMinutes, recordHours;

    public Text TimeCounter, TimeRecord;

    // Start is called before the first frame update
    void Start()
    {
        seconds = 0;
        minutes = 0;
        hours = 0;

        switch (PlayerPrefs.GetInt("dificulty"))
        {
            case 1:
                convertRecordTime(PlayerPrefs.GetInt("easyRecord", 0));
                break;
            case 2:
                convertRecordTime(PlayerPrefs.GetInt("mediumRecord", 0));
                break;
            case 3:
                convertRecordTime(PlayerPrefs.GetInt("hardRecord", 0));
                break;
            default:
                Debug.LogError("Wrong dificulty in TimeCount");
                break;
        }
        TimeRecord.text = recordHours.ToString("D2") + ":" + recordMinutes.ToString("D2") + ":" + recordSeconds.ToString("D2");

        StartCoroutine(waitForStart());
    }

    IEnumerator waitForStart()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(countTime());
    }
    IEnumerator countTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            increaseTime();
            updateTimeCounter();
        }
    }

    private void updateTimeCounter()
    {
        TimeCounter.text = hours.ToString("D2") + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2");
    }

    private void increaseTime()
    {
        if (seconds < 59)
        {
            seconds++;
        }
        else
        {
            seconds = 0;
            if (minutes < 59)
            {
                minutes++;
            }
            else
            {
                minutes = 0;
                if (hours < 59)
                {
                    hours++;
                }
            }
        }
    }

    public int getTimeInSeconds()
    {
        return (hours * 3600 + minutes * 60 + seconds);
    }

    public void convertRecordTime(int time)
    {
        if (time == 0)
        {
            recordHours = 59;
            recordMinutes = 59;
            recordSeconds = 59;
        }
        else
        {
            recordHours = time / 3600;

            time -= recordHours * 3600;

            recordMinutes = time / 60;

            time -= recordMinutes * 60;

            recordSeconds = time;
        }
    }
}
