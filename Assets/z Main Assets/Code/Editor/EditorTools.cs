#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;

public class EditorTools
{
    [MenuItem("Tools/Clear PlayerPrefs")]
    private static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Tools/In Game/Screenshot")]
    private static void Screenshot()
    {
        ScreenCapture.CaptureScreenshot(Application.productName + "-" + DateTime.Now.ToString("hhmmss") + ".png");
        Debug.Log("CLICK: " + Application.productName + "-" + DateTime.Now.ToString("hhmmss") + ".png");
    }
}
#endif