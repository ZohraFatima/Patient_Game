using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.UI;
using Text = UnityEngine.UI.Text;
public class TextChangeEvent : MonoBehaviour
{

    public Text speed_slider;
    public Slider mySlider;

    public void Update()
    {
       mySlider.onValueChanged.AddListener(delegate { myTextChanged(); });
    }

    public void myTextChanged()
    {
        speed_slider.text = mySlider.value.ToString();
    }
}