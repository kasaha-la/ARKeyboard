using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEffectController : MonoBehaviour
{
    private float timeToLive = 0.3f;
    private float currentTime = 0;
    private Vector3 size;

    void Awake()
    {
        // 初期サイズ取得
        size = new Vector3(KeyboardLayout.body.sizeX / 1000, 0.001f, KeyboardLayout.body.sizeZ / 1000);
    }

    void Update()
    {
        DeleteEffect();
    }

    void DeleteEffect()
    {
        if (currentTime < timeToLive)
        {
            Vector3 nowSize = size;
            Vector3 nowPos = gameObject.transform.localPosition;
            float timeRasio = (timeToLive - currentTime) / timeToLive;

            // 0->最大値の半分->0とサイズを変更
            nowSize.x = (((KeyboardLayout.body.sizeX) - Mathf.Abs((timeToLive / 2 - currentTime) / (timeToLive / 2)) * (KeyboardLayout.body.sizeX)) * 0.001f) / 2;
            gameObject.transform.localScale = nowSize;
            
            //　右から左へ移動
            nowPos.x = (((KeyboardLayout.body.sizeX * timeRasio) - KeyboardLayout.body.sizeX / 2) * 0.001f);
            gameObject.transform.localPosition = nowPos;
            
            currentTime += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
