﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// *対応キャラクターに記録された入力を送る
/// </summary>
public class PC_Inst_Control : MonoBehaviour
{

    //*
    //* REQ
    //* 
    //* PERFORM START AND END EVENTS
    //* SEND INPUT TO CONTROLLABLES
    //*
    //*


    /// <summary>
    ///*今の時間から見て次の入力
    ///* 
    /// </summary>
    public PC_Inst_Timeline.Input_Event_st NextInput;
    /// <summary>

    private PC_Inst_Timeline.Input_Event_st PrevInput;

    ///*現在時間
    /// </summary>
    private float LocalTimer;

    /// <summary>
    ///*入力の記録
    /// </summary>
    private PC_Inst_Timeline Timeline_Ref;
    //*INTERFACE TO INSTANCE TIMELINE

    /// <summary>
    /// *初期座標　リセット用
    /// </summary>
    private Vector3 DefPos;
    /// <summary>
    /// *対応キャラクター
    /// </summary>
    private PC_Base ActorRef;




    void Start()
    {
        ActorRef = GetComponent<PC_Base>();


        Timeline_Ref = GetComponentInParent<PC_Inst_Timeline>();
        NextInput = new PC_Inst_Timeline.Input_Event_st();
        NextInput.type = PC_Control.Input_st.NULL;
        DefPos = GetComponent<Transform>().position;
    }

    void OnEnable()
    {
        TL_TimeLineMng.OnReset += ResetEvent;
    }
    void OnDisable()
    {
        TL_TimeLineMng.OnReset -= ResetEvent;
    }

    void ResetEvent()
    {
        GetComponent<Transform>().position = DefPos;
        NextInput = Timeline_Ref.GetZero();
    }

    void Update()
    {



        LocalTimer = TL_TimeLineMng.ctime;




        //*If NO EVENT FOUND
        if (NextInput.type == PC_Control.Input_st.NULL)
        {
         //   NextInput = Timeline_Ref.GetZero();
        }


        //*IF EVENT START IS REACHED
        if (NextInput.start <= LocalTimer)
        {
            UpdateInputStates();

        }



    }
    void UpdateInputStates()
    {
        ///*対応したキャラクターに入力の情報を送る

        if (NextInput.type != PC_Control.Input_st.Over)
        {
            if (NextInput.type != PC_Control.Input_st.NULL)
            {
                ActorRef.DispatchEvent(((int)NextInput.type));
            }
            NextInput = Timeline_Ref.FindNext(NextInput);
        }
    }

    public void CutException()
    {
        NextInput = Timeline_Ref.FindNext();
    }



}
