using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private PlayerControl playerControl;

    void Start()
    {
        background.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData pointerData)
    {
        background.transform.localPosition = pointerData.position;
        background.gameObject.SetActive(true);
		//Debug.Log(pointerData);
	}

    public void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        playerControl.Jump();
    }
}
