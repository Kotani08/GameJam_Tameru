using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public FloatingJoystick floatingJoystick;
    [SerializeField]
    private Rigidbody2D rb;

    public Vector2 horizontal;

    [SerializeField]
    private GameObject Stick;

    [SerializeField]
    private GameObject Button;

    [SerializeField]
    private float jumpPower = 1000;
    private float jumpPowerMax = 3000;
    public float jumpCount=0;
    public float jumpMax=2;
    [SerializeField]
    private Text text;

    public bool stopMove = false;

    private bool JampCharge = false;
    [SerializeField]
    private GameObject Gage;

    public Vector2 JumpPosition;
    public Vector3 FastPoj;
    public Animator anim;

    void Start()
    {
        FastPoj = rb.transform.localPosition;
        JumpReset();
    }
    void Update()
    {
        if(rb.velocity.x != 0f && Stick.activeInHierarchy != true)
        {
          rb.velocity = new Vector2(rb.velocity.x*0.98f,rb.velocity.y);
        }
        if(rb.velocity.y != 0f && Button.activeInHierarchy != true)
        {
            //rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y*0.98f);
            //Debug.Log(rb.velocity.y);
            //text.text = rb.velocity.y.ToString();
        }
        if(JampCharge == true)
        {
            if(jumpPower <= jumpPowerMax)
            {
                if(Gage.transform.localPosition.y <= -3.6f)
                {
                float y = Gage.transform.localPosition.y+0.01f;
                Gage.transform.localPosition = new Vector2(0,y);
                }
            jumpPower += 4.2f;
            }
        }
        if(JampCharge == false && Gage.transform.localPosition.y >= -8.5f)
        {
            float y = Gage.transform.localPosition.y-0.1f;
            Gage.transform.localPosition = new Vector2(0,y);
        }
        if(Gage.transform.localPosition.y < -8.5f)
        {
            Gage.transform.localPosition = new Vector2(0,-8.5f);
        }
        if(stopMove==false){Playerwalk();}
    }

    #region 移動関連
    public void Playerwalk()
    {
      #region 移動関連
      float x = Input.GetAxisRaw("Horizontal");
      horizontal = new Vector2(x,0);
      rb.AddForce(horizontal* speed * Time.fixedDeltaTime);

      #region 後追加の走る処理
    if (x > 0) 
    {
      rb.transform.localScale = new Vector3(2, 2, 2);
        anim.SetBool("run", true);
    }
    else if (x < 0) 
    {
      rb.transform.localScale = new Vector3(-2, 2, 2);
        anim.SetBool("run", true);
    }
    else
    {
        //anim.SetBool("run", false);
    }
    #endregion

      //movingDirecion = new Vector2(x,z);
	  //movingDirecion.Normalize();
	  //movingVelocity = movingDirecion * speed;
      if(Input.GetKeyDown(KeyCode.Space)){JumpCharge();}
      if (Input.GetKeyUp(KeyCode.Space)) {Jump();}
      //new input systemで使ってたやつ
      if (Gamepad.current != null)
      {
        if (Gamepad.current.buttonSouth.wasPressedThisFrame){JumpCharge();}
        if (Gamepad.current.buttonSouth.wasReleasedThisFrame){Jump();}
      }
       
      //rb.velocity = new Vector2(movingVelocity.x, player.velocity.y);
      #endregion
    }
    #endregion

    #region ジャンプ
    public void JumpCharge()
    {
        JampCharge = true;
        JumpPosition = rb.transform.position;
    }

    public void Jump()
    {
      anim.SetBool("jump", true);
        JampCharge=false;
      if(jumpCount >=0)
      {
        jumpCount-=1;
      rb.AddForce(new Vector2(0,jumpPower));
      }
      jumpPower = 1000;
    }
    public void JumpReset()
    {
      anim.SetBool("jump", false);
        jumpCount = jumpMax-1;
    }
    #endregion

    #region ゴール時の処理
    private void Gool()
    {
      stopMove = true;
      //soundManager.PlaySe(ClearSE);
    }
    #endregion

    #region ぶつかった時の処理
    private void OnTriggerEnter2D(Collider2D other)
    {
        //障害物とぶつかった時用
        switch(other.tag)
        {
          case "Goal":
          Gool();
          break;
          case "Out":
          break;
          default:
          break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //ジャンプ回数を地面に振れた時に回復させる用
        if(other.gameObject.tag == "Normal")
        {
            JumpReset();
            return;
        }
    }
    #endregion

    private void FixedUpdate()
    {
      if(horizontal.x <= 1f ||horizontal.x >= -1f){
      horizontal = new Vector2(floatingJoystick.Horizontal*2f,0);
      if (horizontal.x > 0) 
      {
        rb.transform.localScale = new Vector3(2, 2, 2);
        anim.SetBool("run", true);
      }
      else if (horizontal.x < 0) 
      {
        rb.transform.localScale = new Vector3(-2, 2, 2);
        anim.SetBool("run", true);
      }
      else
      {
        anim.SetBool("run", false);
      }
      }
      rb.AddForce(horizontal* speed * Time.fixedDeltaTime);
    }

    public void Reset()
    {
      //Time.fixedDeltaTime = 0f;
      //floatingJoystick.OnPointerUp();
      floatingJoystick.transform.Find("Background").gameObject.SetActive(false);
      floatingJoystick.transform.Find ("Background").transform.Find ("Handle").localPosition =new Vector2(0,0);
      
      Button.SetActive(false);
      Gage.transform.localPosition = new Vector2(0,-8.5f);
      jumpPower = 1000;
      JampCharge = false;
    }
}
