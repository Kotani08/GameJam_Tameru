using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerObject : MonoBehaviour
{
    //触れられたら消えるのと

    private void OnTriggerEnter2D(Collider2D other)
    {
        this.gameObject.SetActive(false);
    }
}
