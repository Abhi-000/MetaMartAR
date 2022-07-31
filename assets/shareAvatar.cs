using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shareAvatar : MonoBehaviour
{
    public Camera cam;
    Texture2D ss;
    private void Start() {
        GetComponent<Animation>().Play("scanAndPLace");
    }
    public void share(){
   StartCoroutine(TakeSS());
        StartCoroutine(ClickShare());
    }
    IEnumerator TakeSS()
    {         
        //Camera viewRoomCam;
        //viewRoomCam = MeasurementController.instance.Camera.GetComponent<Camera>();
        yield return new  WaitForEndOfFrame();
            ss = new Texture2D(cam.pixelWidth, cam.pixelHeight, TextureFormat.RGB24, false);
            ss.ReadPixels(new Rect(0, 0, cam.pixelWidth, cam.pixelHeight), 0, 0);
    }
    private IEnumerator ClickShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = System.IO.Path.Combine(Application.temporaryCachePath, "shared img.png");
        System.IO.File.WriteAllBytes(filePath, ss.EncodeToPNG());
        Destroy(ss);
        new NativeShare().AddFile(filePath).SetSubject("AR Home Decor").
        SetText("Hey check out this amazing outfit made using MetaMart, join me using https://abhi-000.github.io/MetaCommerce/").Share();
    }
}
