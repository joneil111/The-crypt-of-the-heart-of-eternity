using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class TimerController : MonoBehaviour
{
    public static TimerController instance;

    public Text timerCounter;

    private TimeSpan timeplaying;
    private bool timerGoing;

    private float elapsedTime;

    public void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        timerCounter.text = "Time: 00:00.00";
        
        //BeginTimer();
    }

    public void BeginTimer()
    {
        timerGoing = true;
        //startTime = Time.time;
        elapsedTime = 0f;
        

        StartCoroutine(UpdateTimer());

    }

    public void Pause()
    {
        timerGoing = false;
    }

    public void Resume()
    {
        timerGoing = true;
        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
    }


    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timeplaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Time: " + timeplaying.ToString("mm':'ss'.'ff");
            timerCounter.text = timePlayingStr;

            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
