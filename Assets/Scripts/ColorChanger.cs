using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    Image currentSprite;
    public Sprite[] Playbutton;
    int RandomButton;
    float lerpDuration =2f;
    // Start is called before the first frame update
    void Start()
    {
        currentSprite = GetComponent<Image>();
        StartCoroutine(StartChange());

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeButton()
    {
    }

    IEnumerator StartChange() {
        RandomButton = Random.Range(0, Playbutton.Length);
        bool ImageSet = false;
        float TimeElapsed = 0;
        while (TimeElapsed < lerpDuration) {
            if (!ImageSet)
            {
                currentSprite.sprite = Playbutton[RandomButton];
                ImageSet = true;
            }
           TimeElapsed += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(StartChange());
    }
}
