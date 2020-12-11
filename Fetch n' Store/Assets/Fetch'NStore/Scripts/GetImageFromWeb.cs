using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GetImageFromWeb : MonoBehaviour
{

    Texture DownloadedTexture;

     public  RawImage OriginalImage;

    public string ImageWebDataUrl;

    public bool RunAtStartup;


     void Start()
    {
        if (RunAtStartup) { GetImageFromWebURL(); }                 // if the Run At Startup Check-box is checked (true) in the Editor, then upon startup, this image replacement will run.
    }



    public void GetImageFromWebURL()
    {
        StartCoroutine(GetTextureFromOnlineImage());                // Starts the process which manages the fetch of the web-stored image. 
    }



    IEnumerator GetTextureFromOnlineImage()                        // This is the automated process which manages the fetching of the web-stored image. 
    {

        string imagelocation = ImageWebDataUrl;

        using (var uwr = new UnityWebRequest(imagelocation, UnityWebRequest.kHttpVerbGET))
        {
            uwr.downloadHandler = new DownloadHandlerTexture();
            yield return uwr.SendWebRequest();
            OriginalImage.GetComponent<RawImage>().texture = DownloadHandlerTexture.GetContent(uwr);
            Debug.Log("Just replaced the original image on object: " + this.gameObject.name + " with a downloaded image file from web location: " + imagelocation);
        }
        

        UnityWebRequest www = UnityWebRequest.Get(imagelocation);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
          
        }

    }
}
