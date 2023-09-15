using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AnotherFileBrowser.Windows;
using UnityEngine.Networking;

public class ApperanceSetting : MonoBehaviour
{
    public Color32[] colours;
    public RawImage[] rawImage;
    public Image[] primary;
    public Image[] secondary;

    public void OpenFileBrowser()
    {
        var bp = new BrowserProperties();
        bp.filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, path =>
        {
            StartCoroutine(LoadImage(path));
        });
    }

    private IEnumerator LoadImage(string path)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path))
        {
            yield return uwr.SendWebRequest();

            if(uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                var uwrTexture = DownloadHandlerTexture.GetContent(uwr);

                for (int i = 0; i < rawImage.Length; i++)
                {
                    rawImage[i].texture = uwrTexture;
                }
                
            }
        }
    }

    private void ChangeColor(Image[] type, Color32 color)
    {
        for (int i = 0; i < type.Length; i++)
        {
            type[i].color = color;
        }
    }

    public void PrimaryRed()
    {
        ChangeColor(primary, colours[0]);
    }
    public void PrimaryYellow()
    {
        ChangeColor(primary, colours[1]);
    }
    public void PrimaryGreen()
    {
        ChangeColor(primary, colours[2]);
    }
    public void PrimaryBlue()
    {
        ChangeColor(primary, colours[3]);
    }

    public void SecondaryRed()
    {
        ChangeColor(secondary, colours[4]);
    }
    public void SecondaryYellow()
    {
        ChangeColor(secondary, colours[5]);
    }
    public void SecondaryGreen()
    {
        ChangeColor(secondary, colours[6]);
    }
    public void SecondaryBlue()
    {
        ChangeColor(secondary, colours[7]);
    }
}
