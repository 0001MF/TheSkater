using DxCoder;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
