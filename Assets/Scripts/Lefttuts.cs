using DxCoder;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Lefttuts : MonoBehaviour, IPointerDownHandler
{
    public GameObject Right, Left;

   

    public void OnPointerDown(PointerEventData eventData)
    {
        ResetTime();
        // Button is being held down
        Debug.Log("Button is held down.");
    }

    public void ResetTime()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        Time.timeScale = 1f;
        Left.SetActive(false);
    }
}
