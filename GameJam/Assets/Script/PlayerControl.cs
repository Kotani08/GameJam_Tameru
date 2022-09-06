using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControl : MonoBehaviour
{
    public float speed;
    public FloatingJoystick floatingJoystick;
    public Rigidbody2D rb;
    public Vector2 horizontal;
    public GameObject Stick;
    public GameObject Button;
    public float jumpPower;
    
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
    }
    void Update()
    {
        if(rb.velocity.x != 0f && Stick.activeInHierarchy != true)
        {
            rb.velocity = new Vector2(rb.velocity.x*0.98f,rb.velocity.y);
        }
        if(rb.velocity.y != 0f && Button.activeInHierarchy != true)
        {
            rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y*0.98f);
            //Debug.Log(rb.velocity.y);
            text.text = rb.velocity.y.ToString();
        }
    }

    private void FixedUpdate()
    {
        horizontal = new Vector2(floatingJoystick.Horizontal,0);
        rb.AddForce(horizontal* speed * Time.fixedDeltaTime);
    }
    
    public void Jump()
    {
        rb.AddForce(new Vector2(0,jumpPower));
    }

}
