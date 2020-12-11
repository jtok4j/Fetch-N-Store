using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateVideoNameText : MonoBehaviour
{
    public GameObject TheVideoPlayerObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ManuallyUpdateText()
    {
        if (this.GetComponent<Text>().text != null)
        {
            this.GetComponent<Text>().text = "Current Video Clip: " + TheVideoPlayerObject.GetComponent<UnityEngine.Video.VideoPlayer>().clip.name;
        }
    }
}
