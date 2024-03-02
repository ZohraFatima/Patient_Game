using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;
using Text = UnityEngine.UI.Text;
using System.Threading;
using System;
public class user_inputs : MonoBehaviour
{
    public InputField input_timer; // input timer value in seconds 
    public InputField input_break_time; // input break time in seconds
    public Slider slider1;// slider to select the number of trials range[1-10]
    public Slider slider2;// slider to select the speed1 in mm/s range[1-100]
    public Slider slider3;// slider to select the speed2 in mm/s range[1-10]
    public Slider slider4;// slider to select the number of repetitions range[1-10]
    public Text speed1_slider; //text to display the speed1 value of the slider selected by the user 
    public Text speed2_slider;//text to display the speed2 value of the slider selected by the user
    public Text repetitions_slider;//text to display the repetitions value of the slider selected by the user
    public Text trials_slider;//text to display the trials value of the slider selected by the user
    public static string timer_value; // variable to store the timer value in the code 
    public static string speed1_value;// variable to store the speed1 value in the code  
    public static string speed2_value;// variable to store the speed2 value in the code 
    public static string repetitions_value;// variable to store the repetitions value in the code 
    public bool already_exists = false; // a boolean value to check if the values from the canvas are updated in the code variables 
    public static float feasible_time;//minimum time requried by the user to play the game as per the parameters entered by the user 
    public static float d = 0.3f;
    public static float total_distance;// total distance to be travelled by the user or the circle to cover the repetitions with the given speeds.
    public Button yourButton;//A button to switch to the playing scene once all the parameters are entered
    public Text min_time;// a text label to display the minimum feasible time to the user in case if user entersthe timer value less than the minimum
    public float corner_distance;//distance from the origin to the corner point 
    public Vector2 origin = new Vector2(0f, 0f);
   public ValuesScriptable vs;//scriptable object created to store the user inputs

    void Start()
    {

       

    }
    
    void Update()
    {
        //events to sense that user is trying to change the slider value.
         slider1.onValueChanged.AddListener(delegate { myTextChanged1(); });
         slider2.onValueChanged.AddListener(delegate { myTextChanged2(); });
         slider3.onValueChanged.AddListener(delegate { myTextChanged3(); });
         slider4.onValueChanged.AddListener(delegate { myTextChanged4(); });
        yourButton.onClick.AddListener(() => //button event 
        {
            Saveinfo();// function call to save all the user inputs to the scriptable objects 

        });
        // a condtion to check if the values exist if so then feasible time is calculated 
        if (already_exists)
        {
            
            
            float s1 = vs.speed1_value;
            float s2 = vs.speed2_value;
            float r= vs.repetitions_value;
           

            Debug.Log("Timer val" + timer_value);
            already_exists = false; //once the values are recieved for the calculation turning this to false so that update method should not call it again and again 
            Vector2 dist = new Vector2(0.21216f, 0.21216f);// cordinates of the corner point 
            corner_distance = Vector2.Distance(dist, origin);//distance from the origin to the corner point 
            Debug.Log(corner_distance);
            Debug.Log((2 * r) * (2 * d));
            total_distance = (2*(2*r) *(2*corner_distance)) + ((2 * r) * (2 * d));// total distance is the sum of the distance from the origin to the centre point and from the origin to the corner points each multiplied by the number of repetitions for each speed given 
            Debug.Log(total_distance);
            feasible_time = (total_distance * (s1 + s2)) / (2 * s1 * s2); //time is total distance divided by the average speed 
            if (vs.timer_value < feasible_time)// condition to check if the timer value entered by the user is feasible or not 
            { 
                //if the timer value is not feasible a message is displayed to the user to enter the minimum time as calculated feasible time 
                min_time.text = "Your timer value is less than the minimum required. Please enter minimum"+  Math.Ceiling(feasible_time).ToString() +"seconds or more than this.";
            }
            else
            {
                // if the timer value is greater than or equal to the feasible time user is taken to playing scene 
                SceneManager.LoadScene("Player_Action");
            }
            

        }
        
    }
    //a function to save all the user inputs into scriptable objects called once the already_exists become true in the update function
    void Saveinfo()
    { 

        // storing the user inputs into the scriptable object by converting them to their respective datatype as required by the program 
        Debug.Log("function is called");
       
        vs.timer_value = float.Parse(input_timer.text);
        vs.speed1_value = float.Parse(slider1.value.ToString())/1000;
        vs.speed2_value = float.Parse(slider2.value.ToString())/1000;
        vs.repetitions_value = int.Parse(slider3.value.ToString());
        vs.break_time =float.Parse(input_break_time.text);
        vs.trials =int.Parse(slider4.value.ToString());
           already_exists = true; //validating that all the values are now saved in the scriptable objects 
    }


    // functions to update the slider text value called by the onValueChanged() event when the value is changed by the user 
    public void myTextChanged1()
    {
        speed1_slider.text = slider1.value.ToString();
    }
    public void myTextChanged2()
    {
        speed2_slider.text = slider2.value.ToString();
    }
    public void myTextChanged3()
    {
        repetitions_slider.text = slider3.value.ToString();
    }
    public void myTextChanged4()
    {
       trials_slider.text = slider4.value.ToString();
    }

}
