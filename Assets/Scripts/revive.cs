using DxCoder;
//using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class revive : MonoBehaviour
{

    public int delayTime;
    public TextMeshProUGUI[] numberTexts;
    public GameObject ReviveUI;
    public GameObject ReviveWait;
    // Start is called before the first frame update
    private void OnEnable()
    {
        // Called every time the GameObject is enabled
        foreach (var text in numberTexts)
        {

            text.gameObject.SetActive(false);
        }
        StartCoroutine(StartTimerCoroutineNum());
       // SoundManager.Instance.PlayMusic(SoundManager.Instance.Clock);

    }
    private IEnumerator StartTimerCoroutineNum()
    {
        // Iterate through the numbers
        for (int i = 0; i < numberTexts.Length; i++)
        {
            // Enable the Text GameObject for the current number
            numberTexts[i].gameObject.SetActive(true);
            SoundManager.Instance.PlaySound(SoundManager.Instance.Clock);

            // Update the Text component with the current number
            //numberTexts[i].text = i.ToString();

            // Wait for a certain duration before displaying the next number
            yield return new WaitForSeconds(1f); // Change the duration as per your needs

            // Disable the Text GameObject for the current number
            numberTexts[i].gameObject.SetActive(false);
        }

        // Timer finished
       // GameManager.instance.isPaused = false;
       // ReviveUI.SetActive(false);  
        GameManager.instance.GameEnd();
    }

    public void ReviveButton()
    {
      //  SoundManager.Instance.PlayMusic(SoundManager.Instance.Game);

        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        GameManager.instance.ShowRewardAdRevive();
        ReviveWait.SetActive(true);
        ReviveUI.SetActive(false);


    }

   
}
