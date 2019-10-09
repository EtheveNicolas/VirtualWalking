﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class QuestDebug : MonoBehaviour
{

    bool inMenu;
    Text logText;
    public static QuestDebug Instance;

    private Text sliderText;
    private float sliderValue;
    private Canvas UICanvas;
    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UICanvas = GetComponent<Canvas>();
        var rt = DebugUIBuilder.instance.AddLabel("Debug");
        logText = rt.GetComponent<Text>();

        var sliderPrefab = DebugUIBuilder.instance.AddSlider("Slider", 0f, 100.0f, SliderPressed, true);
        var textElementsInSlider = sliderPrefab.GetComponentsInChildren<Text>();
        Assert.AreEqual(textElementsInSlider.Length, 2, "Slider prefab format requires 2 text components (label + value)");
        sliderText = textElementsInSlider[1];
        Assert.IsNotNull(sliderText, "No text component on slider prefab");
        sliderText.text = sliderPrefab.GetComponentInChildren<Slider>().value.ToString();
    }
    
    //Copied from DebugUISample.cs script
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two) || OVRInput.GetDown(OVRInput.Button.Start))
        {
            /*if (inMenu) DebugUIBuilder.instance.Hide();
            else DebugUIBuilder.instance.Show();
            inMenu = !inMenu;*/
            DebugUIBuilder.instance.Show();
            if (inMenu)
            {
                UICanvas.enabled = false;
            }
            else
            {
                UICanvas.enabled = true;
            }
            inMenu = !inMenu;
        }
    }

    public void Log(string msg)
    {
        logText.text = msg;
    }

    public void SliderPressed(float f)
    {
        Debug.Log("Slider: " + f);
        sliderText.text = f.ToString();
    }

    public float GetSliderValue()
    {        
        return float.Parse(sliderText.text) / 100.0f;
    }
}