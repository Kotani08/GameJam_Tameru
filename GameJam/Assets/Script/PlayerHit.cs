using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [SerializeField]
    private PlayerControl playerControl;
    [SerializeField]
    private GameObject ReStartImage;
    [SerializeField]
    private GameObject Result;

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private CountDown Timer;

    [SerializeField]
    private GameObject FloatingButton;

    [SerializeField]
    private GameObject FloatingStick;

    [SerializeField]
    private GameObject TimerObject;

    public Transform ten;
    private bool tenflag =false;

    void Start()
    {
        ReStartImage.SetActive(false);
        Result.SetActive(false);
    }

    void Update()
    {
        if(Timer.CountDownTime <= 0.0F)
        {
            Player.SetActive(false);
            playerControl.stopMove = true;
            ReStartImage.SetActive(true);
            playerControl.floatingJoystick.transform.Find("Background").gameObject.SetActive(false);
            playerControl.floatingJoystick.transform.Find ("Background").transform.Find ("Handle").localPosition =new Vector2(0,0);
        }
        if(ten.transform.localPosition.y <= 545f && tenflag == true)
        {
            ten.transform.localPosition += new Vector3(0,0.2f,0);
        }
        if(ten.transform.localPosition.y >= 545f)
        {
            ten.gameObject.SetActive(false);
            ten.transform.localPosition = new Vector3(0,460f,0);
            tenflag = false;
        }
        
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        //ジャンプ回数を地面に振れた時に回復させる用
        if(other.gameObject.tag == "Normal")
        {
            playerControl.JumpReset();
            return;
        }
        if(other.gameObject.tag == "Out")
        {
            Timer.TimerStop = true;
            playerControl.stopMove = true;
            Player.SetActive(false);
            FloatingButton.SetActive(false);
            FloatingStick.SetActive(false);
            playerControl.stopMove = true;
            ReStartImage.SetActive(true);
            playerControl.floatingJoystick.transform.Find("Background").gameObject.SetActive(false);
            playerControl.floatingJoystick.transform.Find ("Background").transform.Find ("Handle").localPosition =new Vector2(0,0);
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Gool")
        {
            //リザルトの表示
            Timer.TimerStop = true;
            playerControl.stopMove = true;
            Result.SetActive(true);
            return;
        }
        if(other.gameObject.tag == "Timer")
        {
            tenflag = true;
            Timer.CountDownTime += 10f;
            ten.gameObject.SetActive(true);
            return;
        }
    }
    public void ReTry()
    {
        Player.transform.localPosition = playerControl.FastPoj;
        Player.SetActive(true);
        FloatingButton.SetActive(true);
        FloatingStick.SetActive(true);
        ReStartImage.SetActive(false);
        Result.SetActive(false);
        playerControl.stopMove = false;
        Timer.CountDownTimeMax += 10f;
        Timer.CountDownTime = Timer.CountDownTimeMax;
        Timer.TimerStop = false;
        foreach(Transform child in TimerObject.transform)
        {
            GameObject childObject = child.gameObject;
            childObject.SetActive(true);
        }
        playerControl.Reset();
    }
}
