using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adding the scriptable object to the Asset Menu 
[CreateAssetMenu(fileName = "ValuesScriptable", menuName = "ScriptableObjects/ValuesScriptable")]
public class ValuesScriptable : ScriptableObject
{
    //creating variables of the scriptable objects 
    public float timer_value;
    public float speed1_value;
    public float speed2_value;
    public int repetitions_value;
    public int trials;
    public float break_time;
}
