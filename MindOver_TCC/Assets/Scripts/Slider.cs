using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    public SliderJoint2D slider;
    public JointMotor2D aux;
    internal float value;

    void Start()
    {
        aux = slider.motor;
    }
    void Update()
    {
        if(slider.limitState == JointLimitState2D.LowerLimit)
        {
            aux.motorSpeed = 1;
            slider.motor = aux;
        }

        if (slider.limitState == JointLimitState2D.UpperLimit)
        {
            aux.motorSpeed = -1;
            slider.motor = aux;
        }
    }
}
