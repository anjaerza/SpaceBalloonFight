using UnityEngine;
using UnityEditor;

public class CMMenuItems
{
    [MenuItem("CM_Tools/Clear PlayerPrefs")]
    private static void NewMenuOption()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("CM_Tools/Take Screenshot")]
    private static void NewMenuOption2()
    {
        CMScreenshot.Instance.TakeScreenshot("/pic1");
    }

    [MenuItem("CM_Tools/Save Custom Lastlevel")]
    private static void NewMenuOption3()
    {
        GuardadoSerializado.Instance.GuardarLastLevel(7);
    }
}