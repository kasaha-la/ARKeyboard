using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// <summary>
// キーボード全体の動作を制御します。
// </summary>
public class KeyboardController : MonoBehaviour
{
    // 変数
    //  コンポーネント変数
    //      オブジェクト
    public GameObject textMeshProObj;
    public GameObject keyObj;
    public GameObject spaceEffect;
    public GameObject deleteEffect;
    //      オーディオ
    public AudioClip SoundEnter;
    public AudioClip soundSpace;
    public AudioClip soundKeydown_01;
    public AudioClip soundKeydown_02;
    public AudioClip soundDelete;

    //  コンポーネント
    AudioSource audioSource;

    //  内部
    private string bufferStr = "";
    private TextMeshPro textMeshPro;
    private float convertMilitoUnitWeight = 0.001f;

    void Start()
    {
        textMeshPro = textMeshProObj.GetComponent<TextMeshPro>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // エンター押下時：バッファ文字を出現
        if (Input.GetKeyDown(KeyCode.Return))
        {
            System.Random random = new System.Random();
            TextMeshPro aText = new TextMeshPro();
            Vector3 lkeyPos = new Vector3(0, 0, 0);

            aText = createTextInstance(aText, textMeshPro, lkeyPos, this.transform.localRotation, bufferStr);
            aText.GetComponent<KeyTextController>().isEnter = true;
            aText.fontSize *= 1.5f;
            bufferStr = "";

            // 押下音再生
            audioSource.PlayOneShot(SoundEnter);
        }
        // スペース押下時：スペース独自エフェクト
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 pos = new Vector3(0, 0, 0) * convertMilitoUnitWeight;
            GameObject gObj = new GameObject();
            Vector3 gObjSize;

            // 値設定
            Vector3 lkeyPos = new Vector3(-1 * KeyboardLayout.body.sizeX / 2, 0, KeyboardLayout.body.sizeZ / 2) * convertMilitoUnitWeight;
            lkeyPos += (Vector3)KeyboardLayout.layout["space"][0] * convertMilitoUnitWeight;
            gObjSize = (Vector3)KeyboardLayout.layout["space"][1] * convertMilitoUnitWeight;
            bufferStr += " ";
            
            // 作成
            gObj = createObjectInstance(gObj, spaceEffect, lkeyPos, this.transform.localRotation, gObjSize);

            // 押下音再生
            audioSource.PlayOneShot(soundKeydown_01);
        }
        // デリート押下時：バッファ削除
        else if ((Input.GetKeyDown(KeyCode.Backspace)) || (Input.GetKeyDown(KeyCode.Delete)))
        {
            if (bufferStr.Length > 0)
            {
                Vector3 pos = new Vector3(0, 0, 0) * convertMilitoUnitWeight;
                GameObject gObj = new GameObject();

                Vector3 lkeyPos = new Vector3(1 * KeyboardLayout.body.sizeX / 2, 0.02f, 0) * convertMilitoUnitWeight;
                gObj = createObjectInstance(gObj, deleteEffect, lkeyPos, this.transform.localRotation, Vector3.zero);
                bufferStr = "";
                // 押下音再生
                audioSource.PlayOneShot(soundDelete);
            }

        }
        // エスケープキー押下時：全部出現
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (KeyValuePair<string, ArrayList> kvp in KeyboardLayout.layout)
            {
                System.Random random = new System.Random();
                TextMeshPro aText = new TextMeshPro();

                Vector3 lkeyPos = new Vector3(-1 * KeyboardLayout.body.sizeX / 2, 0, KeyboardLayout.body.sizeZ / 2) * convertMilitoUnitWeight;
                lkeyPos += ((Vector3)kvp.Value[0]) * convertMilitoUnitWeight;

                aText = createTextInstance(aText, textMeshPro, lkeyPos, this.transform.localRotation, kvp.Key.ToString());
                aText.color = new Color((float)random.NextDouble() * 0.9f + 0.1f, (float)random.NextDouble() * 0.9f + 0.1f, (float)random.NextDouble() * 0.9f + 0.1f, (float)random.NextDouble() * 0.1f + 0.9f);
            }
            // 押下音再生
            audioSource.PlayOneShot(soundSpace);
        }
        //　その他通常キー押下時：出現＋バッファ
        else
        {
            foreach (char c in Input.inputString)
            {
                ArrayList arKey = new ArrayList();
                string tmpStr;
                TextMeshPro aText = new TextMeshPro();

                Vector3 lkeyPos = new Vector3(-1 * KeyboardLayout.body.sizeX / 2, 0, KeyboardLayout.body.sizeZ / 2) * convertMilitoUnitWeight;

                // 押下キーが設定から取得できた場合
                if (KeyboardLayout.layout.TryGetValue(c.ToString(), out arKey))
                {
                    lkeyPos += (Vector3)arKey[0] * convertMilitoUnitWeight;
                    tmpStr = c.ToString();
                    bufferStr += c;
                }
                else
                {
                    lkeyPos += new Vector3(KeyboardLayout.body.sizeX / 2, 0, -1 * KeyboardLayout.body.sizeZ / 2) * convertMilitoUnitWeight;
                    tmpStr = "><";

                }
                aText = createTextInstance(aText, textMeshPro, lkeyPos, this.transform.localRotation, tmpStr);

                // 押下音再生
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

        // GameObjectを作成+設定
        GameObject createObjectInstance(GameObject gObj, GameObject instance, Vector3 pos, Quaternion rot, Vector3 scale)
        {
            gObj = Instantiate(instance, pos, Quaternion.identity, this.transform);
            gObj.transform.localPosition = pos;
            gObj.transform.localRotation = rot;
            gObj.transform.localScale = scale;
            return gObj;
        }

        // TextMeshProを作成+設定
        TextMeshPro createTextInstance(TextMeshPro txtObj, TextMeshPro instance, Vector3 pos, Quaternion rot, string text)
        {
            txtObj = Instantiate(instance, pos, Quaternion.identity, this.transform);
            txtObj.transform.localPosition = pos;
            txtObj.transform.localRotation = rot;
            txtObj.text = text;
            return txtObj;
        }

    }
}
