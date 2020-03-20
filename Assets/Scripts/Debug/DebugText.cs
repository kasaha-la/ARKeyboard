#define DEBUG_MODE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugText : MonoBehaviour
{
    public static DebugText instance { get; private set; }
    TextMeshProUGUI textMeshPro;
    // static bool debugMode = false;

    void Awake()
    {
        instance = this;
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
    }

    private void SetValue(string text)
    {				      
        textMeshPro.text = text;
    }

    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Log(string text){
        // if(debugMode){
            DebugText.instance.SetValue(text);
        // }
    }

}
