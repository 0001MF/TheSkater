using DxCoder;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Right, Left;
    public Button RightButton, Leftbutton;
    void Start()
    {
       

        // Add a listener to the button's OnClick event
      //  RightButton.onClick.AddListener(ShowLeft);
      //  Leftbutton.onClick.AddListener(ResetTime);

        Time.timeScale = 0.5f;
        Right.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

   
}
