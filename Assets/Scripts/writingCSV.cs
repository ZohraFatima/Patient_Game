using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;
using Application = UnityEngine.Application;

public class writingCSV : MonoBehaviour
{
    
    public static string filepath; // to store the current filepath 
    public StreamWriter writer;
    public bool checker = false;
    public circle_movement cm;
    
    //method to create the file , called in the runtimer coroutine once the timer starts the file is created
    public void create_file(int z)
    {
        filepath = Path.Combine(Application.dataPath, "Trail" + (z + 1).ToString() + ".csv");// Filename will include trial number hence z+1, where z refers to reach trial 
        writer = new StreamWriter(filepath, false);//file creation
        if (writer.BaseStream.Length == 0)// adding the headers to each column if they are not present 
            WriteToCSV("Timestamp,Player_X,Player_Y,Circle_X,Circle_Y,Point1_X,Point1_Y,Point2_X,Point2_Y,Point3_X,Point3_Y");

    }
    // method to close the file called in the runtimer coroutine once the time is up and the positions are shuffled
    public void close_file()
    {
        if (writer != null)
        {
            writer.Close();
            writer = null;


        }
    }

    //method to write the values in the file, timestamp,player position and the circle position 
    public void LogPosition()
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        WriteToCSV($"{timestamp},{Player_Movement.target.x},{Player_Movement.target.y},{cm.circle.position.x},{cm.circle.position.y},{circle_movement.initialPositions[0].x},{circle_movement.initialPositions[0].y},{circle_movement.initialPositions[1].x},{circle_movement.initialPositions[1].y},{circle_movement.initialPositions[2].x},{circle_movement.initialPositions[2].y}");
    }

    //metohd to write each row in the file called by the logposition method 
    public void WriteToCSV(string line)
    {
        if (writer != null)
        {
            writer.WriteLine(line);
        }
    }

}