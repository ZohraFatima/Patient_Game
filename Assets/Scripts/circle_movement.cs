using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;
using System.Net.Mail;
using System.IO;
using System.Threading;
using System.Timers;


public class circle_movement : MonoBehaviour
{
    
    public Transform[] gameObjects;  // storing the live positions of the three points 
    public static Vector2[] initialPositions;// storing the initial positions of the three points 
    public Vector2[] permananetPositions;//storing the fixed positions of the three points
    public Transform circle;// circle gameobject 
    public Transform dot;// player game object 
    public int j;
    public Vector2 moveDir;//direction of the circle towards the three points 
    public AudioClip vibrationSound; //sound to be played when the player goes out of the circle 
    private AudioSource audioSource;

    public Vector2 origin = new Vector2(0f, 0f);//origin 
    public  List<float> speeds =new List<float>();// an arraylist of speeds to store the values of the speeds entered by the user 
    public  List<int> repetitions = new List<int>();// an arraylist of repetitions to store the values of the repetitions entered by the user 
    
    public  ValuesScriptable vs;//scriptable object to refer to the userinput values 
    public timer t;//timer class object to call the timer and move the circle 






    void Start()
    {
        
        //referring to the scriptable objects for the values and storing them in the class variables 
        speeds.Add(vs.speed1_value);
        speeds.Add(vs.speed2_value);
        repetitions.Add(vs.repetitions_value);
        Debug.Log("ss"+speeds.Count);

        //initializing the arrays to store the position values 
        initialPositions = new Vector2[gameObjects.Length];
        permananetPositions = new Vector2[gameObjects.Length];

        for (j = 0; j < gameObjects.Length; j++)
        {
            initialPositions[j] = gameObjects[j].position;
            permananetPositions[j] = gameObjects[j].position;
        }

        //code for audiosource to be activated 
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = vibrationSound;
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.volume = 0.5f; // Adjust the volume as needed
       
}

    // Update is called once per frame
    void Update()
    {
       //to check if the player is within the boundries of the circle 
        if (IsInBounds())
        {
           
            audioSource.Play();
        }
        else
        {
            
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
       
    }
    // a coroutine to move the circle to all the three points 
    public IEnumerator MoveCircle()
    {
        
        // a for loop to go to each point 
        for (int x = 0; x < permananetPositions.Length; x++)
        {
           // a foreach loop to move for each speed 
            foreach (float sp in speeds)
            {
               //a loop to move for each repetition
               for(int y=0; y < repetitions[0];y++) 
                {
                    //it calls another coroutine by passing the position of the point and the speed to actually move the circle 
                    yield return StartCoroutine(moving_circle(permananetPositions[x], sp));
                }
            }
        }

}
    //a coroutine to shuffle the position of the three points after each trial 
    public IEnumerator ShufflePositions()
    {
        
        for (int i = initialPositions.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);//it generates a random value from the given range and assign it as index 
            Vector2 temp = initialPositions[i];
            initialPositions[i] = initialPositions[randomIndex];
            initialPositions[randomIndex] = temp;
            Debug.Log("shuffling");
            yield return null;

        }

       
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].position = initialPositions[i];
            Debug.Log("shuffling");
            yield return null;

        }
        //after shuffling both plaer and circle comes back to the origin at the end of each trial 
        circle.position = origin;
        dot.position = origin;
    }
    //a function to check weather the player is in the bounds of the circle or not 
    bool IsInBounds()
    {
        if (circle != null)
        {
            float distance = Vector2.Distance(dot.position, circle.position);
            float combinedRadii = dot.localScale.x / 2 + circle.localScale.x / 2;

            return distance < combinedRadii;
        }

        return false;
    }
    //the coroutine that actually moves the cirle towars the points 
    public IEnumerator moving_circle(Vector2 points, float speed)
    {
        

        //from the origin towards the points 
        while (Vector2.Distance((Vector2)circle.position, points) > 0.01f)//it calculates the distance between the circle and the point 
        {
            moveDir = (points - (Vector2)circle.position).normalized;//determines the direction where the circle has to move 
            circle.Translate(moveDir * speed * Time.deltaTime);//moves the circle 
            yield return null;
        }
        circle.position = points;//then once the circle is very close to the point  with less than 0.01f distance with the point it sets the circle on the point 
       
       //from the point towards the origin 
        while (Vector2.Distance((Vector2)circle.position, origin) > 0.01f)
        {
            moveDir = (origin - (Vector2)circle.position).normalized;
            circle.Translate(moveDir * speed * Time.deltaTime);
            yield return null;
        }
        circle.position = origin;//then once the circle is very close to the origin with less than 0.01f distance with the origin it sets the circle on the origin  
       



    }
}




