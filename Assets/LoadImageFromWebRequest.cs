using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class LoadImageFromWebRequest : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(FetchViaTexture());
    }

    IEnumerator FetchViaTexture()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://pbs.twimg.com/profile_images/877787847247503360/DHVlmPfz.jpg");

        Debug.Log("Downloading...");
        yield return www.SendWebRequest();

        Debug.Log("Done");

        Texture2D downloadedTexture = DownloadHandlerTexture.GetContent(www);
        Sprite sprite = Sprite.Create(downloadedTexture, new Rect(0.0f, 0.0f, downloadedTexture.width, downloadedTexture.height), new Vector2(0.5f, 0.5f), 100.0f);

        GetComponent<Image>().sprite = sprite;
    }
}
