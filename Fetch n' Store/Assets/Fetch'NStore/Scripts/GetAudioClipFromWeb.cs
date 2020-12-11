using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class GetAudioClipFromWeb : MonoBehaviour
{
    public GameObject AudioSourceObject;
    public string AudioWebDataURL;
    public AudioType AudioFileType;
    public bool RunAtStartup;                           // Check this in the inspector to have the audio fetch run upon startup.
    private void Start()
    {
        if (RunAtStartup) { ManuallyFetchAudioClip(); }
    }

    public void ManuallyFetchAudioClip()
    {
        StartCoroutine(GetAudioClip());
    }

    // Update is called once per frame
    IEnumerator GetAudioClip()
    {
        using (var uwr = UnityWebRequestMultimedia.GetAudioClip(AudioWebDataURL, AudioFileType))     //
        {
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.LogError(uwr.error);
                yield break;
            }

            AudioClip clip = DownloadHandlerAudioClip.GetContent(uwr);
            AudioSourceObject.GetComponent<AudioSource>().clip = clip;

            Debug.Log("Just replaced the original audioclip on object: " + this.gameObject.name + " with a downloaded audio file from web location: " + AudioWebDataURL);

        }

    }
}
