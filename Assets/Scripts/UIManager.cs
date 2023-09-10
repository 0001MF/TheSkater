
using UnityEngine;
using DxCoder;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI CurrentLevelText;
    public TextMeshProUGUI Coin;
    int CurrentLevel;

    // Start is called before the first frame update
    void Start()
    {
        CurrentLevel = PlayerPrefs.GetInt("levelReached", 0);
        CurrentLevel = CurrentLevel-1;
        CurrentLevelText.text = "" + (CurrentLevel);
        Coin.text = "" + CoinManager.Instance.Coins;
        SoundManager.Instance.PlaySound(SoundManager.Instance.GameWin);
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
    public void PlaySound()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);

    }
}
