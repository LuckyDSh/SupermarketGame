﻿/*
* TickLuck Team
* All rights reserved
*/

using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    #region Variables 
    [HideInInspector] public Slider s_meltBarSlider;
    //[SerializeField] private float maxValue;
    //public static float maxTime = 0;
    public Gradient gradient;
    public Image fill;

    [HideInInspector]
    public bool isOn;
    #endregion

    #region UnityMethods
    private void Start()
    {
        //maxTime = maxValue;

        s_meltBarSlider = gameObject.GetComponent<Slider>();
        //s_meltBarSlider.value = maxTime;
        isOn = true;
    }

    private void Update()
    {
        //if (isOn)
        //{
        //    maxTime = PlayerStackColorController.forwardForce;

        //    if (maxTime > s_meltBarSlider.maxValue)
        //        PlayerStackColorController.forwardForce = s_meltBarSlider.maxValue;

        //    SetProgress(maxTime);
        //}
    }
    #endregion

    public void UpdateTheBar(float value)
    {
        //maxTime += value;
        SetProgress(value);
    }

    public void SetMaxValue(int maxvalue)
    {
        s_meltBarSlider.maxValue = maxvalue;
        s_meltBarSlider.value = maxvalue;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetProgress(float value)
    {
        s_meltBarSlider.value = value;

        fill.color = gradient.Evaluate(s_meltBarSlider.normalizedValue);//
    }
}
