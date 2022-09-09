using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public float CountDownTime; // カウントダウンタイム
    public float CountDownTimeMax = 30.0f;
    public Text TextCountDown; // 表示用テキストUI
    public bool TimerStop =false;



    void Start () {
    CountDownTime = CountDownTimeMax;    // カウントダウン開始秒数をセット
    }

    void Update ()
    {
        if(TimerStop == false){
        // カウントダウンタイムを整形して表示
        TextCountDown.text = String.Format("{0:00.00}", CountDownTime);
        // 経過時刻を引いていく
        CountDownTime -= Time.deltaTime;
    
        if (CountDownTime <= 0.0F)
        {
        CountDownTime = 0.0F;
        }
    }
    }
}
