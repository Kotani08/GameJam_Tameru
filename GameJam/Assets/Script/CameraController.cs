using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    GameObject player;
    float Y;

    // Use this for initialization
void Start()
{
        // Playerの部分はカメラが追いかけたいオブジェクトの名前をいれる
        this.player = GameObject.Find("Player");
}

    // Update is called once per frame
void Update()
{
    Vector3 playerPos = this.player.transform.localPosition;
    transform.localPosition = new Vector3(playerPos.x, Y, transform.localPosition.z);
    if(playerPos.y <= 800f){Y = -2.631958f;}
    if(playerPos.y >= 800f){Y =1500f;}
    if(playerPos.y >= 2300f){Y =3000f;}
    if(playerPos.y >= 3800f){Y =4500f;}
    if(playerPos.y >= 5300f){Y =6000f;}
    if(playerPos.y >= 6800f){Y =7500f;}
    if(playerPos.y >= 8300f){Y =9000f;}
}
}