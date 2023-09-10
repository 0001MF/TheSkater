using DxCoder;
//using MoreMountains.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Righttuts : MonoBehaviour, IPointerDownHandler
{
    public GameObject Right, Left;

    public void OnPointerDown(PointerEventData eventData)
    {
        ShowLeft();
        // Button is being held down
        Debug.Log("Button is held down.");
    }

    public void ShowLeft()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        StartCoroutine(ShowLeftWait());

    }

    IEnumerator ShowLeftWait()
    {
        yield return new WaitForSeconds(1f);
       // MMVibrationManager.Haptic(HapticTypes.Warning);

        Right.SetActive(false);
        Left.SetActive(true);
    }
}