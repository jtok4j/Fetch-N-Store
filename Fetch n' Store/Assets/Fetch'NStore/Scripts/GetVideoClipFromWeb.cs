using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetVideoClipFromWeb : MonoBehaviour
{
    public GameObject ObjectWithVideoPlayer;
    public string VideoWebDataURL;
    public bool RunAtStartup;
    public Text TextCurrentVideoClipName;
    void Start()
    {
        if (RunAtStartup) { GetVideoClipFromWebURL(); }
    }

    // Update is called once per frame
    public void GetVideoClipFromWebURL()
    {
        var vp = ObjectWithVideoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>();
        vp.url = VideoWebDataURL;

        if (TextCurrentVideoClipName != null){ TextCurrentVideoClipName.text = " Current Video Clip: " + ObjectWithVideoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().url; }
                                             // above line checks to see if we've assigned a Text component to write an update of the file name it's using to. If not, it doesn't update the filename on-screen.
        vp.Play();
    }
}
