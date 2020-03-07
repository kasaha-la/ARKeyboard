using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugText : MonoBehaviour
{
    public static DebugText instance { get; private set; }
    TextMeshProUGUI textMeshPro;

    void Awake()
    {
        instance = this;
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
    }

    public void SetValue(string text)
    {				      
        textMeshPro.text = text;
    }

}
