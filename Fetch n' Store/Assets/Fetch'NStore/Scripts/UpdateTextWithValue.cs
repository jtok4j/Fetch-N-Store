using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTextWithValue : MonoBehaviour
{
    public Text TheTextToUpdate;
    public GameObject TheObjectToPullFileNameFfrom;
    
    void Start()
    {
        TheTextToUpdate.text = ("Current Audio Clip: " + TheObjectToPullFileNameFfrom.GetComponent<AudioSource>().clip.name);
    }

    // Update is called once per frame
    public void ManuallyUpdateText()
    {
        // TheTextToUpdate.text = ("Current Audio Clip: " + TheObjectToPullFileNameFfrom.GetComponent<AudioSource>().clip.name);
        TheTextToUpdate.text = ("Current Audio Clip: " + GameObject.Find("AudioArea").GetComponent<GetAudioClipFromWeb>().AudioWebDataURL);
    }
}
