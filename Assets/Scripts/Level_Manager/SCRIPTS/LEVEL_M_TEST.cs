using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DxCoder;

public class LEVEL_M_TEST : MonoBehaviour {

    public GameObject LoadingScene;


    [System.Serializable]
    public class Level
    {
       
        public string LevelText;
        public int Unlock;
        public bool isInteractible;

        public Button.ButtonClickedEvent OnClick;
    }
    public GameObject LEVELButton;
    public Transform Spacer;
    public List<Level> LevelList;

    // Use this for initialization
    void Start ()
    {
        //  Delete();
        int isUnlocked = PlayerPrefs.GetInt("Unlockalllevel", 0);
        if (isUnlocked == 1)
        {
            UnlockAllLevel();

        }
        else { 
        FillList();
        }
        

	}
	void FillList()
    {
        foreach(var level in LevelList)
        {
            
            GameObject newbutton = Instantiate(LEVELButton) as GameObject;
            level_button_new button = newbutton.GetComponent<level_button_new>();

            button.LevelText.text =  level.LevelText;

            if (PlayerPrefs.GetInt("Level" + button.LevelText.text) == 1)
            {
                level.Unlock = 1;
                level.isInteractible = true;
            }

            button.unlocked = level.Unlock;
            button.GetComponent<Button>().interactable = level.isInteractible;
          //  button.GetComponent<Button>().onClick.AddListener(() => LoadLevel("Level" + button.LevelText.text));
            button.GetComponent<Button>().onClick.AddListener(() => LoadLevel(button.LevelText.text));
            //button.GetComponent<Button>().onClick.AddListener(() => StarCor("Level" + button.LevelText.text));



            newbutton.transform.SetParent(Spacer);
        }
        SAVE();
    }


    void SAVE()
    {
        {
            GameObject[] allbuttons = GameObject.FindGameObjectsWithTag("LevelButton");
            foreach (GameObject buttons in allbuttons)
            {
                level_button_new button = buttons.GetComponent<level_button_new>();
                PlayerPrefs.SetInt("Level" + button.LevelText.text, button.unlocked);
            }
        }
        
    }

    public void UnlockAllLevel() {
        foreach (var level in LevelList)
        {

            GameObject newbutton = Instantiate(LEVELButton) as GameObject;
            level_button_new button = newbutton.GetComponent<level_button_new>();

            button.LevelText.text = level.LevelText;

           
                level.Unlock = 1;
                level.isInteractible = true;
            

            button.unlocked = level.Unlock;
            button.GetComponent<Button>().interactable = level.isInteractible;
            //  button.GetComponent<Button>().onClick.AddListener(() => LoadLevel("Level" + button.LevelText.text));
            button.GetComponent<Button>().onClick.AddListener(() => LoadLevel(button.LevelText.text));
            newbutton.transform.SetParent(Spacer);
        }
        }

    public void Delete()
    {
        PlayerPrefs.DeleteAll();
    }


    void LoadLevel(string value)
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        PlayerPrefs.SetInt("OpenLevel",int.Parse(value));
        SceneManager.LoadScene("Game");
        //AsyncOperation operation = SceneManager.LoadSceneAsync(value);

       // LoadingScene.SetActive(true);
    }
    
}
