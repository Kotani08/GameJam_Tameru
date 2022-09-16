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

    [SerializeField]
    private GameObject StartCastObject;

    public Animator anim;

    #region 音関連

    [SerializeField]
    AudioClip StartBGM;

    [SerializeField]
    SoundManager soundManager;

    [SerializeField]
    AudioClip DamageSE;

    [SerializeField]
    AudioClip ItemGetSE;

    [SerializeField]
    AudioClip ClearSE;

    [SerializeField]
    AudioClip MissSE;

    private float RetryCount=0f;


    #endregion

    public Transform ten;
    private bool tenflag =false;

    void Start()
    {
        // /soundManager.PlayBgm(StartBGM);
        ReStartImage.SetActive(false);
        Result.SetActive(false);
        RetryCount=0f;
        ReSetTry();
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
            ten.transform.localPosition += new Vector3(0,0.4f,0);
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
            anim.SetBool("out", true);
            soundManager.PlaySe(DamageSE);
            Timer.TimerStop = true;
            playerControl.stopMove = true;
            FloatingButton.SetActive(false);
            FloatingStick.SetActive(false);
            playerControl.floatingJoystick.transform.Find("Background").gameObject.SetActive(false);
            playerControl.floatingJoystick.transform.Find ("Background").transform.Find ("Handle").localPosition =new Vector2(0,0);
            Invoke("ResultEnd", 0.5f);
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "Gool")
        {
            if(Timer.TimerStop == false){
            soundManager.PlayResult(ClearSE);
            Timer.TimerStop = true;
            playerControl.stopMove = true;
            Result.SetActive(true);
            //数字を大きくしたい
            //float Score = RetryCount*10+Mathf.Floor(Timer.CountDownTime) /100;
            Invoke("GoolEnd", 1f);
            }
            return;
        }
        if(other.gameObject.tag == "Timer")
        {
            RetryCount += 1.00f;
            soundManager.PlaySe(ItemGetSE);
            tenflag = true;
            Timer.CountDownTime += 10f;
            Timer.CountDownTimeMax += 10f;
            ten.gameObject.SetActive(true);
            return;
        }
    }
    public void ReTry()
    {
        StartCast();
        Invoke("ReTrySt",1f);
    }

    public void ReSetTry()
    {
        StartCast();
        Invoke("ReSetTrySt",1f);
    }
    
    private void ResultEnd()
    {
        ReStartImage.SetActive(true);
        soundManager.PlayResult(MissSE);
    }

    private void GoolEnd()
    {
        float Score = Mathf.Abs((Timer.CountDownTimeMax-Timer.CountDownTime)*100f-10000f);
            Score = Score-RetryCount*1000;
            if(Timer.CountDownTimeMax >= 100f){Score=0;}
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking (Score);
        Invoke("GoolEndGet", 0.5f);
    }
    private void GoolEndGet()
    {
        Result.transform.Find("Button").gameObject.SetActive(true);
    }

    private void StartCast()
    {
        Timer.TimerStop = true;
        playerControl.stopMove = true;
        Player.transform.localPosition = playerControl.FastPoj;
        anim.SetBool("out", false);
        StartCastObject.SetActive(true);
        ReStartImage.SetActive(false);
        Result.SetActive(false);
        ReStartImage.SetActive(false);
        FloatingButton.SetActive(true);
        FloatingStick.SetActive(true);
    }

    private void ReTrySt()
    {
        StartCastObject.SetActive(false);
        RetryCount += 1.00f;
        soundManager.PlayBgm(StartBGM);
        Player.SetActive(true);
        FloatingButton.SetActive(true);
        FloatingStick.SetActive(true);
        playerControl.stopMove = false;
        Timer.CountDownTimeMax += 10f;
        Timer.CountDownTime = Timer.CountDownTimeMax;
        Timer.TimerStop = false;
        foreach(Transform child in TimerObject.transform)
        {
            GameObject childObject = child.gameObject;
            childObject.SetActive(true);
        }
    }

    private void ReSetTrySt()
    {
        StartCastObject.SetActive(false);
        RetryCount = 0f;
        soundManager.PlayBgm(StartBGM);
        Player.SetActive(true);
        FloatingButton.SetActive(true);
        FloatingStick.SetActive(true);
        playerControl.stopMove = false;
        Timer.CountDownTimeMax = 30f;
        Timer.CountDownTime = Timer.CountDownTimeMax;
        Timer.TimerStop = false;
        Result.transform.Find("Button").gameObject.SetActive(false);
        foreach(Transform child in TimerObject.transform)
        {
            GameObject childObject = child.gameObject;
            childObject.SetActive(true);
        }
        playerControl.Reset();
    }
}
