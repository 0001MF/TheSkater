using DxCoder;
//using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReviveWait : MonoBehaviour
{

    public int delayTime;
    public TextMeshProUGUI[] numberTexts;
    public GameObject ReviewWaitUI;
    private void OnEnable()
    {   
        foreach (var text in numberTexts)
        {

            text.gameObject.SetActive(false);
        }
        StartCoroutine(StartTimerCoroutineNum2());
     

    }
    private IEnumerator StartTimerCoroutineNum2()
    {
       // GameManager.instance.isPaused = true;

        for (int i = 0; i < numberTexts.Length; i++)
        {
            numberTexts[i].gameObject.SetActive(true);
            SoundManager.Instance.PlaySound(SoundManager.Instance.go);
            yield return new WaitForSeconds(1.5f);
            numberTexts[i].gameObject.SetActive(false);
        }
        GameManager.instance.isPaused = false;

        ReviewWaitUI.SetActive(false);
    }

   


}
