using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdInitialize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       MobileAds.Initialize(initStatus => { });
    }

    // Update is called once per frame
    
}
