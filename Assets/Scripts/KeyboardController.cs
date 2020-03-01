using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 文字の出現を制御するためのクラスです
public class KeyboardController : MonoBehaviour
{
    public GameObject textMeshProObj;
    private TextMeshPro textMeshPro;
    Rigidbody rigidbody;
    private float convertMilitoUnitWeight = 0.001f;
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
            if (KeyboardLayout.keyLayout.TryGetValue(c.ToString(),out arKey))
            {
                TextMeshPro aText = Instantiate(textMeshPro,
                (Vector3)arKey[0] * convertMilitoUnitWeight + new Vector3(0, 0, rigidbody.position.z),
                Quaternion.identity);
                aText.text = c.ToString();
            }else{
                TextMeshPro aText = Instantiate(textMeshPro,
                rigidbody.position,
                Quaternion.identity);
                aText.text = "ないです";
            }
        }
    }
}
