using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

public class circle_movement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] gameObjects;  // Assign your game objects in the Inspector
    public static Vector2[] initialPositions;
    public Vector2[] permananetPositions;
    public Transform circle;
    public Transform dot;
    public int j;
    public Vector2 moveDir;
    float speed = float.Parse(user_inputs.speed1_value);
    float speed2 = float.Parse(user_inputs.speed2_value);
    public static int eachrepeat = int.Parse(user_inputs.repetitions_value);
    public AudioClip vibrationSound; // Assign the vibration sound in the Unity Editor
    private AudioSource audioSource;

    public Vector2 origin = new Vector2(0f, 0f);
    void Start()
    {
        initialPositions = new Vector2[gameObjects.Length];
        permananetPositions = new Vector2[gameObjects.Length];
        for (j = 0; j < gameObjects.Length; j++)
        {
            initialPositions[j] = gameObjects[j].position;
            permananetPositions[j] = gameObjects[j].position;
        }
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = vibrationSound;
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.volume = 0.5f; // Adjust the volume as needed
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInBounds())
        {
            //Debug.Log("Smaller circle is in the bounds of the larger circle.");
            audioSource.Play();
        }
        else
        {
            // Debug.Log("Smaller circle is outside the bounds of the larger circle.");
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
    }
    public IEnumerator MoveCircle()
    {
        // Debug.Log("move");
        for (int i = 0; i < permananetPositions.Length; i++)
        {
            // Debug.Log("imp");
           
            for (int j = 0; j < eachrepeat; j++)
            {
                yield return StartCoroutine(moving_circle(permananetPositions[i], speed));

            }
            for (int k = 0; k < eachrepeat; k++)
            {
                yield return StartCoroutine(moving_circle(permananetPositions[i], speed2));

            }

        }

    }
    public IEnumerator moving_circle(Vector2 points, float speed)
    {

        while (Vector2.Distance((Vector2)circle.position, points) > 0.01f)
        {
            moveDir = (points - (Vector2)circle.position).normalized;
            circle.Translate(moveDir * speed * Time.deltaTime);
            yield return null;
        }
        circle.position = points;
        while (Vector2.Distance((Vector2)circle.position, origin) > 0.01f)
        {
            moveDir = (origin - (Vector2)circle.position).normalized;
            circle.Translate(moveDir * speed * Time.deltaTime);
            yield return null;
        }
        circle.position = origin;



    }
    public IEnumerator ShufflePositions()
    {
        // Shuffle the array of initial positions
        //moveDir = gameObjects[Random.Range(0, 3)].position - circle.position).normalized;
        for (int i = initialPositions.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Vector2 temp = initialPositions[i];
            initialPositions[i] = initialPositions[randomIndex];
            initialPositions[randomIndex] = temp;
            Debug.Log("shuffling");
            yield return null;

        }

        // Apply shuffled positions to the game objects
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].position = initialPositions[i];
            Debug.Log("shuffling");
            yield return null;

        }
        circle.position = origin;
        dot.position = origin;
    }
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
}
