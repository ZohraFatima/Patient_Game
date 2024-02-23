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
    // Start is called before the first frame update
    public static string filepath;
    public StreamWriter writer;
    public bool checker = false;
    public circle_movement cm;

    public void create_file(int z)
    {
        filepath = Path.Combine(Application.dataPath, "Trail" + (z + 1).ToString() + ".csv");
        writer = new StreamWriter(filepath, false);
        if (writer.BaseStream.Length == 0)
            WriteToCSV("Timestamp,Player_X,Player_Y,Circle_X,Circle_Y,Point1_X,Point1_Y,Point2_X,Point2_Y,Point3_X,Point3_Y");

    }
    public void close_file()
    {
        if (writer != null)
        {
            writer.Close();
            writer = null;


        }
    }
    public void LogPosition()
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        WriteToCSV($"{timestamp},{Player_Movement.target.x},{Player_Movement.target.y},{cm.circle.position.x},{cm.circle.position.y},{circle_movement.initialPositions[0].x},{circle_movement.initialPositions[0].y},{circle_movement.initialPositions[1].x},{circle_movement.initialPositions[1].y},{circle_movement.initialPositions[2].x},{circle_movement.initialPositions[2].y}");
    }
    public void WriteToCSV(string line)
    {
        if (writer != null)
        {
            writer.WriteLine(line);
        }
    }

}