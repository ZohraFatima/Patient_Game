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
public class user_inputs : MonoBehaviour
{
    public InputField input_timer;
   // public InputField input_speed1;
   // public InputField input_speed2;
    public Slider slider1;
    public Slider slider2;
    public Slider slider3;
    public Text speed1_slider;
    public Text speed2_slider;
    public Text repetitions_slider;
    // public InputField input_repetitions;
    public static string timer_value;
    public static string speed1_value;
    public static string speed2_value;
    public static string repetitions_value;
    public bool already_exists = false;
    public static float feasible_time;
    public static float d = 0.3f;
    public static float total_distance;
    public Button yourButton;
    public Text min_time;
    public float corner_distance;
    public Vector2 origin = new Vector2(0f, 0f);
    //public Timer t;
    void Start()
    {

       

    }
    
    void Update()
    {
         slider1.onValueChanged.AddListener(delegate { myTextChanged1(); });
         slider2.onValueChanged.AddListener(delegate { myTextChanged2(); });
         slider3.onValueChanged.AddListener(delegate { myTextChanged3(); });
        yourButton.onClick.AddListener(() =>
        {
            Saveinfo();

        });
        if (already_exists)
        {
            
            speed1_value = PlayerPrefs.GetString("speed1_value");
            speed2_value = PlayerPrefs.GetString("speed2_value");
            repetitions_value = PlayerPrefs.GetString("repetitions_value");
            float s1 = float.Parse(speed1_value);
            float s2 = float.Parse(speed2_value);
            float r= float.Parse(repetitions_value);
            //feasible = 0.3 * ();
            timer_value = PlayerPrefs.GetString("timer_value");

            Debug.Log(timer_value);
            already_exists = false;
            Vector2 dist = new Vector2(0.21216f, 0.21216f);
            corner_distance = Vector2.Distance(dist, origin);
            Debug.Log(corner_distance);
            Debug.Log((2 * r) * (2 * d));
            total_distance = (2*(2*r) *(2*corner_distance)) + ((2 * r) * (2 * d));
            Debug.Log(total_distance);
            feasible_time = (total_distance * (s1 + s2)) / (2 * s1 * s2);
            if (float.Parse(timer_value) < feasible_time)
            {
                min_time.text = "Your timer value is less than the minimum required. Please enter minimum"+ feasible_time.ToString() +"seconds or more than this.";
            }
            else
            {
                SceneManager.LoadScene("Player_Action");
            }
            

        }
        
    }
    void Saveinfo()
    {
        Debug.Log("function is called");
        PlayerPrefs.SetString("timer_value", input_timer.text);
        PlayerPrefs.SetString("speed1_value",slider1.value.ToString());
        PlayerPrefs.SetString("speed2_value",slider2.value.ToString());
        PlayerPrefs.SetString("repetitions_value",slider3.value.ToString());
       // PlayerPrefs.Save();
        already_exists = true;
    }
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

}
