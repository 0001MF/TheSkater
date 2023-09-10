using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DxCoder;
using TMPro;
using GamePolygon;
using System;

public class MenuManager : MonoBehaviour {


	public string RateUsUrl,MoreGamesURL;
	public TextMeshProUGUI CurrentLevelText;
    public TextMeshProUGUI Coin;
    public TextMeshProUGUI MusicText;

    [Header("Share")]
    public string Subject;
    public string Body;
    public GameObject SettingPanel;
    int CurrentLevel;
    int coins;
    [Header("Reward")]
    public GameObject dailyRewardBtn;
    public TextMeshProUGUI dailyRewardBtnText;
    public GameObject rewardUI;
    Animator dailyRewardAnimator;
    // Use this for initialization
    private const string FirstTimeKey = "FirstTime";
    void Start () {
      
            CurrentLevel = PlayerPrefs.GetInt("levelReached", 0);
        if (CurrentLevel == 0) {
            PlayerPrefs.SetInt("levelReached", 1);
            CurrentLevel = PlayerPrefs.GetInt("levelReached", 0);
        }
            CurrentLevelText.text = "" + (CurrentLevel);
        SoundManager.Instance.PlayMusic(SoundManager.Instance.Menu);
          
             StartCoroutine(getCoin());
        dailyRewardAnimator = dailyRewardBtn.GetComponent<Animator>();
        Adcontrol.instance.HideBanner();


    }

    public void Update () {


        if (!DailyRewardController.Instance.disable && dailyRewardBtn.gameObject.activeSelf)
        {
            if (DailyRewardController.Instance.CanRewardNow())
            {
                dailyRewardBtnText.text = "GRAB YOUR REWARD!";
                dailyRewardAnimator.SetTrigger("activate");
            }
            else
            {
                TimeSpan timeToReward = DailyRewardController.Instance.TimeUntilReward;
                dailyRewardBtnText.text = string.Format("REWARD IN {0:00}:{1:00}:{2:00}", timeToReward.Hours, timeToReward.Minutes, timeToReward.Seconds);
                dailyRewardAnimator.SetTrigger("deactivate");
            }
        }
    }

	public void LoadLevel(){
        PlaySound();

        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
       
            SceneManager.LoadScene("Game");
           
        
        
	}


	public void RateUs ()
	{
        PlaySound();
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.AduppGames.SkaterMania");

    }


    public void MoreGames()
    {
        PlaySound();

        Application.OpenURL("https://play.google.com/store/apps/developer?id=Adupp+Global+LLC");

    }


    public void Restart()
    {
        PlaySound();

        SceneManager.LoadScene("Game");
    }

    public void Home()
    {
        PlaySound();

        SceneManager.LoadScene("Menu");

    }
    public void Shop()
    {
        PlaySound();

        SceneManager.LoadScene("Shop");

    }

   

    IEnumerator getCoin() {
        yield return new WaitForEndOfFrame();
        Coin.text = "" + CoinManager.Instance.Coins;

    }

    public void MuteSound()
    {
        if (SoundManager.Instance.IsMuted())
        {
            MusicText.text = "MUSIC ON";
        }
        else
        {
            MusicText.text = "MUSIC OFF";

        }
        SoundManager.Instance.ToggleMute();

    }

    public void PlaySound() {
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);

    }

    public void GrabDailyReward()
    {
        if (DailyRewardController.Instance.CanRewardNow())
        {
            int reward = DailyRewardController.Instance.GetRandomReward();

            // Round the number and make it mutiplies of 5 only.
            int roundedReward = (reward / 5) * 5;

            // Show the reward UI
            ShowRewardUI(roundedReward);

            // Update next time for the reward
            DailyRewardController.Instance.ResetNextRewardTime();
        }
    }

    public void ShowRewardUI(int reward)
    {
        rewardUI.SetActive(true);
        rewardUI.GetComponent<RewardUIController>().Reward(reward);
    }

    public void HideRewardUI()
    {
        rewardUI.GetComponent<RewardUIController>().Close();
    }

    public void UnlockAllLevel() {
        PlaySound();

        PlayerPrefs.SetInt("Unlockalllevel", 1);
    }

    public void ClearAllPlayerPrefs()
    {
        PlaySound();

        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All PlayerPrefs cleared.");
    }

    private bool IsFirstTimePlaying()
    {
        // Check if the flag exists in PlayerPrefs
        if (PlayerPrefs.HasKey(FirstTimeKey))
        {
            // The flag exists, indicating that the game has been played before
            return false;
        }

        // The flag does not exist, indicating that this is the first time playing
        return true;
    }
}




