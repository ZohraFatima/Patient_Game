using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;
using System.Runtime.InteropServices;
using System.IO;
using Application = UnityEngine.Application;

public class timer : MonoBehaviour
{

   
    public static float timeRemaining = float.Parse(user_inputs.timer_value); // seconds
    public static int numberOfRepeats = 3;
    public float breakTime = 4; // seconds
    public UnityEngine.UI.Text timeText;
    public bool timerIsRunning = false;
    public UnityEngine.UI.Text trailtext;
    public static int z;
    public writingCSV wc;
    public bool f = false;
    public circle_movement cm;
    private void Start()
    {
       
       
       
        StartCoroutine(RunTimer());



    }

    void Update()
    {
       // if(f)
          wc.LogPosition();
        

    }
    IEnumerator RunTimer()
    {
        for (z = 0; z< numberOfRepeats; z++)
        {
            timerIsRunning = true;
            wc.create_file(z+1);
            //f = true;
            trailtext.text = "Trails : " + (z + 1).ToString();
            yield return StartCoroutine(Countdown());
            yield return StartCoroutine(cm.ShufflePositions());
            wc.close_file();
            yield return new WaitForSeconds(breakTime);
            
            //f = false;
            Debug.Log("wait");
            

        }

        //Debug.Log("Timer has completed all intervals.");
    }

    IEnumerator Countdown()
    {
        
        StartCoroutine(cm.MoveCircle());

        while (timeRemaining > 0)
        {
            
            //cir = circle.position;
            
            Debug.Log("first time");
            Debug.Log("time");
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
            
            yield return null;
        }

        //Debug.Log("Time has run out!");
        timeRemaining = float.Parse(user_inputs.timer_value);
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;
        timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
    }
    
    
    
}
