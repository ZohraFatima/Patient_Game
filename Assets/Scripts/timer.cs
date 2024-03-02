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

    public ValuesScriptable vs;//creating scriptable object to refer to the userinput values 
    public circle_movement c;//circle object to access its methods 
    public float timeRemaining;// seconds
    public int numberOfRepeats;// number of trials entered by the user 
    public float breakTime; // the time pause after each trial entered by the user 
    public UnityEngine.UI.Text timeText;
    public bool timerIsRunning = false;//initially the timer is not running so false
    public UnityEngine.UI.Text trailtext;
    public static int z;
    public writingCSV wc;//a csv file object to acces its methods to write the positions of player and the circle in the file while playing the game 
    public bool f = false;
    
    void Awake()
    {
        //storing the values from scriptable object to the class variable 
        timeRemaining = vs.timer_value;

        numberOfRepeats = vs.trials;
        breakTime = vs.break_time;
    }
    
   
    
    private void Start()
    {


        //starting the game once it reaches the playing scene by called the coroutine 
        StartCoroutine(RunTimer());



    }

    void Update()
    {
       //writing the positions of the cirlce and the player in the file on a live basis 
          wc.LogPosition();
        

    }
    // the coroutine called in the start method to actually start running the game for playing 
    IEnumerator RunTimer()
    {
        //looping for each trial 
        for (z = 0; z< numberOfRepeats; z++)
        {
            timerIsRunning = true;//timer is true i.e it has started 
            wc.create_file(z+1); //creating a file with the trial number 
            trailtext.text = "Trails : " + (z + 1).ToString();//updating the current trial on the screen 
            yield return StartCoroutine(Countdown());//calling the countdown coroutine to run the timer till it reaches zero 
            yield return StartCoroutine(c.ShufflePositions());//shffling the positions once the timer is off 
            wc.close_file();//closing the file 
            yield return new WaitForSeconds(breakTime);// waiting for the breaktime ,another trial will start after break time 
            Debug.Log("wait");
            

        }

        
    }
     //coroutine to make the timer work
    IEnumerator Countdown()
    {

       //corutine to move the circle present in circle_movement class referred from here using the circle_movement object 
        StartCoroutine(c.MoveCircle());
        
        //running the loop till the timer reaches the value zero 
        while (timeRemaining > 0)
        {
            
           
            
            Debug.Log("first time");
            Debug.Log("time");
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);//calling the display method to display the current time on the screen 
            
            yield return null;
        }
        timeRemaining = vs.timer_value;//re-initializing the original timer value to start the next trial 
    }


    //method to display the live value of the timer on the screen
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;
        timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
    }
    
    
    
}
