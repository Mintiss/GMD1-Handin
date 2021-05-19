using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Text volume, volumeText;
    public Slider slider;

    void Start()
    {
        volumeText.text = (PlayerPrefs.GetFloat("volume")).ToString();
        slider.value = PlayerPrefs.GetFloat("volume");
    }

    public void OnSliderChanged(float value)
    {
        volume.text = value.ToString();
    }
}
