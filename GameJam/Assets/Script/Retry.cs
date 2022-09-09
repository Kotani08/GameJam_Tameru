using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Retry : MonoBehaviour,IPointerDownHandler
{
    [SerializeField]
    private PlayerControl playerControl;
    [SerializeField]
    private GameObject ReStartImage;
    [SerializeField]
    private GameObject Result;
    [SerializeField]
    private GameObject Player;

    public void OnPointerDown(PointerEventData pointerData)
    {
        Player.transform.localPosition = playerControl.FastPoj;
        ReStartImage.SetActive(false);
        Result.SetActive(false);
        playerControl.stopMove = false;
    }
}
