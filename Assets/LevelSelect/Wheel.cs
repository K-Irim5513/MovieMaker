using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    Animator ac;
    Transform T;
    void Start()
    {
        T = GetComponent<Transform>();
        ac = GetComponent<Animator>();
    }
    public int selection = 0;
    public int levelcount = 3;

    public bool ended;
    void Update()
    {
        if (UT_InputFilter.GetX())
        {
            ac.SetBool("State", true);

        }

        if (UT_InputFilter.GetD())
        {

            selection = Math.Min(selection + 1, levelcount);
        }
        if (UT_InputFilter.GetU())
        {

            selection = Math.Max(selection - 1, 0);
        }
        if (ended)
        {
            ac.enabled = false;
        }
        Transition();
    }
    const float rotationunit = 360.0f / 8.0f;
    public float rotationrate;
    public float rot;
    private float velo;

    public bool set;

    void Transition()
    {


        velo += (selection * rotationunit) - (rot) * rotationrate;

        velo *= 0.7f;
        rot += velo;
        rot = (rot + velo);

        set = (Math.Abs(T.rotation.eulerAngles.z - (rot)) < 10);


        T.rotation = Quaternion.Euler(0, 0, rot);
    }

}
