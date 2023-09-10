using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DxCoder;
using TMPro;
//using MoreMountains.NiceVibrations;
using GamePolygon;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    int CurrentLevel;
    public GameObject GameOverUI;
    public bool finished;
    public TextMeshProUGUI currentText;
    public GameObject[] Level;

    [Header("Reaction")]
    public GameObject[] Emoji;

    [Header("Control")]
    public GameObject Dpad;
    public int Health;

    ParticleSystem PS1, PS2;

    [HideInInspector]
    public int Cars;
    public Slider slider;
    GameObject Players, Finishline;
    float TotalDistance;
    bool Ready;
    public TextMeshProUGUI CoinsCollected;

    public GameObject[] Hearts;
    public Button skip;
    public TextMeshProUGUI skipText;
    public GameObject NextLevelUI;

    public GameObject[] _environment;
    public GameObject ReviveUI;
    [HideInInspector]
    public bool isPaused;
    public GameObject PlayerObject;

    private const string FirstTimeKey = "FirstTime";
    public GameObject TutorialUI;

    public void Awake()
    {
       
      

        
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        CurrentLevel = PlayerPrefs.GetInt("levelReached", 0);
        Instantiate(Level[CurrentLevel]);
        if (CurrentLevel == 1) {
            TutorialUI.SetActive(true);
        }
        SoundManager.Instance.PlayMusic(SoundManager.Instance.Game);
    }


  
    void Start()
    {

        isPaused = false;
        Ready = false;
        StartCoroutine(SliderAnimate());
        finished = false;
        currentText.text = "" + (CurrentLevel);
       
            Dpad.SetActive(true);
       

        PS1 = GameObject.FindGameObjectWithTag("Blast").GetComponent<ParticleSystem>();
          Adcontrol.instance.HideBanner();
        Health = 0;
        skip.interactable = false;
        if (CurrentLevel <= 50) {
            _environment[0].SetActive(true);
           
        }
        else if (CurrentLevel > 50 && CurrentLevel < 76) {
          
            _environment[1].SetActive(true);
           


        }
        else if(CurrentLevel >= 76)
        {
            _environment[2].SetActive(true);
           

        }
        Adcontrol.instance.LoadAd();

    }


    void Update()
    {
        if (!finished)
        {
            if (Cars == 2 && !finished)
            {
                GameWin();
            }

            if (Ready)
            {
                float distance = Vector3.Distance(Players.transform.position, Finishline.transform.position);
                // Debug.Log("Total Distance :" + TotalDistance + "  Distance : " + distance);
                slider.value = 100 - ((distance / TotalDistance) * 100);

            }
            CoinsCollected.text = "" + CoinManager.Instance.Coins;
        }
        if (!SkipLevelController.Instance.CanRewardNow())
        {
            TimeSpan timeToReward = SkipLevelController.Instance.TimeUntilReward;
            skipText.text = string.Format("{0:00}:{1:00}:{2:00}", timeToReward.Hours, timeToReward.Minutes, timeToReward.Seconds);
        }
    }

    IEnumerator End() {
        yield return new WaitForSeconds(1f);
        GameOverUI.SetActive(true);

    }

    public void GameEnd() {
        if (!finished)
        {
            finished = true;
            Adcontrol.instance.ShowAd();
            //RunEmoji();
            SoundManager.Instance.PlaySound(SoundManager.Instance.GameOVer);
            StartCoroutine(End());
            if (!SkipLevelController.Instance.disable && skip.gameObject.activeSelf)
            {
                if (SkipLevelController.Instance.CanRewardNow())
                {
                    //dailyRewardBtnText.text = "GRAB YOUR REWARD!";
                    // dailyRewardAnimator.SetTrigger("activate");
                    skip.interactable = true;
                }
                else
                {
                    skip.interactable= false;
                    TimeSpan timeToReward = SkipLevelController.Instance.TimeUntilReward;
                    skipText.text = string.Format("{0:00}:{1:00}:{2:00}", timeToReward.Hours, timeToReward.Minutes, timeToReward.Seconds);
                   // dailyRewardAnimator.SetTrigger("deactivate");
                }
            }
        }
    }

    public void GameWin() {
        if (!finished)
        {
            finished = true;
            Adcontrol.instance.ShowAd();
            PS1.Play();
            CurrentLevel = CurrentLevel + 1;

            PlayerPrefs.SetInt("levelReached", CurrentLevel);
           // PlayerPrefs.SetInt("Level" + CurrentLevel.ToString(), 1);
          //  PlayerPrefs.SetInt("OpenLevel", CurrentLevel);

            StartCoroutine(Win());
        }
       
    }


    IEnumerator Win() {
        yield return new WaitForSeconds(0.1f);
        // GameWinUI.SetActive(true);
        SceneManager.LoadScene("GameWin");
    }



    public void Restart() {
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);

        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);

        SceneManager.LoadScene("Menu");
    }

    void RunEmoji() {
       // int RandNum = Random.Range(0, 3);
      //  Emoji[RandNum].SetActive(true);
        Dpad.SetActive(false);
      
    }


    IEnumerator SliderAnimate() {
        yield return new WaitForSeconds(1f);
        Players = GameObject.FindGameObjectWithTag("Players");
        Finishline = GameObject.FindGameObjectWithTag("Finish");
        TotalDistance = Vector3.Distance(Players.transform.position, Finishline.transform.position);
        Ready = true;
    }

    public void HealthControl() {
        if (!finished)
        {
           // MMVibrationManager.Haptic(HapticTypes.Warning);

            Hearts[Health].SetActive(false);
            Health++;
            
            if (Health == 3)
            {
                PlayerObject.SetActive(false);
                ReviveUI.SetActive(false);

                GameEnd();
                return;

            }
            ReviveUI.SetActive(true);

            isPaused = true;
        }
    }

    public void ShowRewardAd() {
        if (Adcontrol.instance.isReawrdLoaded)
        {
            Adcontrol.instance.ShowRewardedAd();
            RewardCoin();
        }
       
    }

    public void RewardCoin()
    {
        SkipLevelController.Instance.ResetNextRewardTime();

        Debug.Log("Ad rewarded! Handling the reward logic here.");
        // Implement your reward logic here
        NextLevelUI.SetActive(true);
        //SoundManager.Instance.PlayMusic(SoundManager.Instance.Menu);

    }

    public void SkipLevel() {
       // SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        CurrentLevel = CurrentLevel + 1;

        PlayerPrefs.SetInt("levelReached", CurrentLevel);
        //PlayerPrefs.SetInt("Level" + CurrentLevel.ToString(), 1);
        //PlayerPrefs.SetInt("OpenLevel", CurrentLevel);
        SceneManager.LoadScene("Game");
    }

    public void ShowRewardAdRevive()
    {
         if (Adcontrol.instance.isReawrdLoaded)
        {
            Adcontrol.instance.ShowRewardedAd();
            //RewardCoin();
        }
        else {
            Adcontrol.instance.ShowAd();
        }

    }
}
