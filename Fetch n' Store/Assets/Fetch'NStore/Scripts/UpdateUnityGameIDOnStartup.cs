using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpdateUnityGameIDOnStartup : MonoBehaviour
{
    public GameObject adsObject;
    public Text TextDisplayGameID;
    public GameObject ButtonShowAd;
    private string thegameid;
    
    void Start()
    {
        HideShowAdButtonIfNotReady();
    }

   
    void Update()
    {
        
    }

    public void ShowGameIDOnScreen()
    {
      

        thegameid = adsObject.GetComponent<ads>().gameid;

        if (String.IsNullOrEmpty(thegameid)) { TextDisplayGameID.text = ("The gameid was not fetched yet. Please click the same button to try again."); } // check if the fetch from the web has finished.

        else 
        {
            ButtonShowAd.SetActive(true);
            TextDisplayGameID.text = ("Current Unity Ads GameID: " + thegameid + " See the Console for the web address it was fetched from by the ads.cs script");
        }
    }


    public void HideShowAdButtonIfNotReady()
    { 
        ButtonShowAd.SetActive(false); 

        }

}
