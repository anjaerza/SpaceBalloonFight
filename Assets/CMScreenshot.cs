using UnityEngine;
using System.Collections;
using System.IO;

public class CMScreenshot : Singleton<CMScreenshot>
{
    private int count = 0;

    public void TakeScreenshot(string picName)
    {
        StartCoroutine(ScreenshotEncode(picName));
        //Debug.Log("Screenshot");
    }

    IEnumerator ScreenshotEncode(string picName)
    {
        // wait for graphics to render
        yield return new WaitForEndOfFrame();

        // create a texture to pass to encoding
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        // put buffer into texture
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();

        // split the process up--ReadPixels() and the GetPixels() call inside of the encoder are both pretty heavy
        yield return 0;

        byte[] bytes = texture.EncodeToPNG();

        // save our test image (could also upload to WWW)
        File.WriteAllBytes(Application.persistentDataPath + "/" + picName + ".png", bytes);
        count++;

        // Added by Karl. - Tell unity to delete the texture, by default it seems to keep hold of it and memory crashes will occur after too many screenshots.
        Destroy(texture);

        //Debug.Log( Application.dataPath + "/../testscreen-" + count + ".png" );
    }
}