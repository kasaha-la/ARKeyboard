using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 文字の出現を制御するためのクラスです
public class KeyboardController : MonoBehaviour
{
    public GameObject textMeshProObj;
    public GameObject keyObj;
    private TextMeshPro textMeshPro;
    Rigidbody rigidbody;
    #if UNITY_EDITOR 
        private float convertMilitoUnitWeight = 0.1f;
    #else
        private float convertMilitoUnitWeight = 0.001f;
    #endif
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = textMeshProObj.GetComponent<TextMeshPro>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (char c in Input.inputString)
        {
            ArrayList arKey = new ArrayList();
            string tmpStr;
            TextMeshPro aText;
            GameObject gObj;
            Vector3 gObjSize;
            Vector3 keyPos = rigidbody.position + new Vector3(-1*KeyboardLayout.body.sizeX/2,0,KeyboardLayout.body.sizeZ/2) * convertMilitoUnitWeight;
            if (KeyboardLayout.layout.TryGetValue(c.ToString(), out arKey))
            {
                keyPos += (Vector3)arKey[0] * convertMilitoUnitWeight;
                gObjSize = (Vector3)arKey[1] * convertMilitoUnitWeight;;
                tmpStr = c.ToString();
            }
            else
            {
                keyPos += new Vector3(KeyboardLayout.body.sizeX/2,0,0) * convertMilitoUnitWeight;
                gObjSize = new Vector3(20,1,20) * convertMilitoUnitWeight;
                tmpStr = "><";
            }
            gObj = Instantiate(keyObj,keyPos,Quaternion.identity);
            gObj.transform.localScale = gObjSize;
            aText = Instantiate(textMeshPro, keyPos, Quaternion.identity);
            aText.text = tmpStr;
        }
    }
}
