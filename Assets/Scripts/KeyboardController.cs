using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 文字の出現を制御するためのクラスです
public class KeyboardController : MonoBehaviour
{
    public GameObject textMeshProObj;
    public GameObject keyObj;
    public GameObject spaceEffect;
    public GameObject deleteEffect;
    private TextMeshPro textMeshPro;
    AudioSource audioSource;
    public AudioClip SoundEnter;
    public AudioClip soundSpace;
    public AudioClip soundKeydown_01;
    public AudioClip soundKeydown_02;
    public AudioClip soundDelete;
    private string str = "";

    Rigidbody rigidbody;
#if UNITY_EDITOR
    private float convertMilitoUnitWeight = 0.001f;
#else
        private float convertMilitoUnitWeight = 0.001f;
#endif
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = textMeshProObj.GetComponent<TextMeshPro>();
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //  DebugText.instance.SetValue(gameObject.transform.position + " " + gameObject.transform.rotation);
        if (Input.GetKeyDown(KeyCode.Return))
        {
            System.Random random = new System.Random();
            TextMeshPro aText;
            Vector3 gObjSize;
            Vector3 lkeyPos = new Vector3(0, 0, 0);

            aText = Instantiate(textMeshPro, lkeyPos, Quaternion.identity, this.transform);
            // aText.color = new Color((float)random.NextDouble() * 0.5f + 0.5f, (float)random.NextDouble() * 0.5f + 0.5f, (float)random.NextDouble() * 0.5f + 0.5f, (float)random.NextDouble() * 0.2f + 0.8f);
            aText.transform.localPosition = lkeyPos;
            aText.GetComponent<TextMeshController>().isEnter = true;
            aText.transform.localRotation = this.transform.localRotation;
            aText.text = str;
            aText.fontSize *= 1.5f;
            str = "";
            audioSource.PlayOneShot(SoundEnter);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            // オブジェクトのアニメーションをやりたい
            Vector3 pos = new Vector3(0, 0, 0) * convertMilitoUnitWeight;
            GameObject gObj;
            Vector3 gObjSize;

            Vector3 lkeyPos = new Vector3(-1 * KeyboardLayout.body.sizeX / 2, 0, KeyboardLayout.body.sizeZ / 2) * convertMilitoUnitWeight;
            lkeyPos += (Vector3)KeyboardLayout.layout["space"][0] * convertMilitoUnitWeight;
            gObjSize = (Vector3)KeyboardLayout.layout["space"][1] * convertMilitoUnitWeight;
            gObj = Instantiate(spaceEffect, lkeyPos, this.transform.localRotation, this.transform);
            gObj.transform.localScale = gObjSize;
            gObj.transform.localPosition = lkeyPos;
            gObj.transform.localRotation = this.transform.localRotation;

            audioSource.PlayOneShot(soundKeydown_01);
            str += " ";
        }
        else if ((Input.GetKeyDown(KeyCode.Backspace)) || (Input.GetKeyDown(KeyCode.Delete)))
        {
            if (str.Length > 0)
            {
                Vector3 pos = new Vector3(0, 0, 0) * convertMilitoUnitWeight;
                GameObject gObj;
                Vector3 gObjSize;

                Vector3 lkeyPos = new Vector3(1 * KeyboardLayout.body.sizeX / 2, 0.02f,0) * convertMilitoUnitWeight;
                // gObjSize = (Vector3)KeyboardLayout.layout["space"][1] * convertMilitoUnitWeight;
                gObj = Instantiate(deleteEffect, lkeyPos, this.transform.localRotation, this.transform);
                gObj.transform.localScale = Vector3.zero;
                gObj.transform.localPosition = lkeyPos;
                gObj.transform.localRotation = this.transform.localRotation;
                str = "";
                audioSource.PlayOneShot(soundDelete);
            }

        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (KeyValuePair<string, ArrayList> kvp in KeyboardLayout.layout)
            {
                System.Random random = new System.Random();
                TextMeshPro aText;
                Vector3 gObjSize;
                Vector3 keyPos = gameObject.transform.position + new Vector3(-1 * KeyboardLayout.body.sizeX / 2, 0, KeyboardLayout.body.sizeZ / 2) * convertMilitoUnitWeight;
                Vector3 lkeyPos = new Vector3(-1 * KeyboardLayout.body.sizeX / 2, 0, KeyboardLayout.body.sizeZ / 2) * convertMilitoUnitWeight;

                keyPos += (Vector3)kvp.Value[0] * convertMilitoUnitWeight;
                gObjSize = (Vector3)kvp.Value[1] * convertMilitoUnitWeight; ;
                lkeyPos += ((Vector3)kvp.Value[0]) * convertMilitoUnitWeight;
                gObjSize = (Vector3)kvp.Value[1] * convertMilitoUnitWeight; ;

                aText = Instantiate(textMeshPro, lkeyPos, Quaternion.identity, this.transform);
                aText.color = new Color((float)random.NextDouble() * 0.9f + 0.1f, (float)random.NextDouble() * 0.9f + 0.1f, (float)random.NextDouble() * 0.9f + 0.1f, (float)random.NextDouble() * 0.1f + 0.9f);
                aText.transform.localPosition = lkeyPos;
                aText.transform.localRotation = this.transform.localRotation;
                aText.text = kvp.Key.ToString();

            }
            audioSource.PlayOneShot(soundSpace);
        }
        else
        {
            foreach (char c in Input.inputString)
            {
                ArrayList arKey = new ArrayList();
                string tmpStr;
                TextMeshPro aText;
                GameObject gObj;
                Vector3 gObjSize;
                Vector3 keyPos = gameObject.transform.position + new Vector3(-1 * KeyboardLayout.body.sizeX / 2, 0, KeyboardLayout.body.sizeZ / 2) * convertMilitoUnitWeight;
                Vector3 lkeyPos = new Vector3(-1 * KeyboardLayout.body.sizeX / 2, 0, KeyboardLayout.body.sizeZ / 2) * convertMilitoUnitWeight;
                if (KeyboardLayout.layout.TryGetValue(c.ToString(), out arKey))
                {
                    keyPos += (Vector3)arKey[0] * convertMilitoUnitWeight;
                    gObjSize = (Vector3)arKey[1] * convertMilitoUnitWeight;
                    lkeyPos += (Vector3)arKey[0] * convertMilitoUnitWeight;
                    tmpStr = c.ToString();
                    str += c;
                }
                else
                {
                    lkeyPos += new Vector3(KeyboardLayout.body.sizeX / 2, 0, -1 * KeyboardLayout.body.sizeZ / 2) * convertMilitoUnitWeight;
                    gObjSize = new Vector3(20, 1, 20) * convertMilitoUnitWeight;
                    tmpStr = "><";
                }
                // gObj = Instantiate(keyObj,lkeyPos,Quaternion.identity,this.transform);
                // gObj.transform.localScale = gObjSize;
                // gObj.transform.localPosition = lkeyPos;
                // gObj.transform.localRotation = this.transform.localRotation;
                aText = Instantiate(textMeshPro, lkeyPos, Quaternion.identity, this.transform);
                aText.transform.localPosition = lkeyPos;
                aText.transform.localRotation = this.transform.localRotation;
                aText.text = tmpStr;


                if (Random.value > 0.5f)
                {
                    audioSource.PlayOneShot(soundKeydown_01);
                }
                else
                {
                    audioSource.PlayOneShot(soundKeydown_02);
                }
            }
        }

    }
}
