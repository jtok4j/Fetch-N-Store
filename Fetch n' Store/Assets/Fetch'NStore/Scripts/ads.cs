using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;
using UnityEngine.Networking;

public class ads : MonoBehaviour
{
    //public GameObject Timer;
    public static ads theadsscript;
    public string gameid = null;

    public bool AdJustFinishedPlaying;

    public string DefaultOrFallbackGameIdForUnityAdsIOS;
    public string DefaultOrFallbackGameIdForUnityAdsAndroid;
    

    public string IOSWebDataURL;
    public string AndroidWebDataURL;
    public bool RunAtStartup;


    //    public GameObject TheMasterVariablesScript;
    void Awake()
    {

        if (theadsscript == null)
        {
            DontDestroyOnLoad(gameObject);
            theadsscript = this;
        }
        else if (theadsscript != this)
        {
            Destroy(gameObject);
        }
    }  

    void Start()
    {      
        if (RunAtStartup) { StartCoroutine(GetUnityAdIdFromOnlineData() ) ; }       // *** Most likely you'll want to have this checked (enabled) in the inspector, so you don't have to manually call
                                                                                    //the ads initialization, as this should be done early/beforehand, so ads will be "ready" later when needed.
    }


    public void ManualFetchOfGameIDFromTheWeb() {
        StartCoroutine(GetUnityAdIdFromOnlineData());
    }



    IEnumerator GetUnityAdIdFromOnlineData()
    {
#if UNITY_IOS
        string adidlocation = IOSWebDataURL ;
#endif
#if UNITY_ANDROID
        string adidlocation = AndroidWebDataURL;
#endif




        UnityWebRequest www = UnityWebRequest.Get(adidlocation);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            SetGameIdIfUnavailableFromWeb();
        }


        else
        {
            gameid = www.downloadHandler.text;
            Debug.Log("Just set the Unity Ads ID to: " + gameid + " through a lookup on the web from: " + adidlocation);

            if (string.IsNullOrEmpty(gameid)) { SetGameIdIfUnavailableFromWeb(); }  //

            AdInitializeAndGetReady();
        }

    }

    private void AdInitializeAndGetReady()
    {
        Advertisement.Initialize(gameid, false);      // ALWAYS ON LIVE MODE!  Set in the account management for Monetization (Unity Dashboard Site) to Test Mode. Turn off from there when launching live!
      
    }



    private void SetGameIdIfUnavailableFromWeb()
    {


#if UNITY_IOS
            string gameid = DefaultOrFallbackGameIdForUnityAdsIOS ;
#endif

#if UNITY_ANDROID
            string gameid = DefaultOrFallbackGameIdForUnityAdsAndroid;
#endif

        Debug.Log("Did not get gameid from the online data location. Using default hard-coded Unity Ad ID's." +
                " Current Game ID for Monetization (Ads) purposes is: " + gameid + " **Note: IOS hard-code is: 3784292 and Android hard-code is: 3784293");

        AdInitializeAndGetReady();
    }






    public void ShowInterstitialAd()
    {

        if (Advertisement.IsReady())
        {
            Debug.Log("Checked, and Ad is ready to show.");
            Advertisement.Show("video", new ShowOptions() { resultCallback = HandleAdResult });

        }
        else
        {

            Debug.Log(" Ad not ready at the moment! Please try again later!");
          
        }
    }


    private void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:

                AdJustFinishedPlaying = true;



                break;

            case ShowResult.Skipped:
                Debug.Log("Player did not fully watch the ad");
                break;

            case ShowResult.Failed:
                Debug.Log("Player failed to launch the ad");
                break;
        }
    }







}