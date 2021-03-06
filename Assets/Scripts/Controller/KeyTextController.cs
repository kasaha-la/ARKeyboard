﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 生成された文字自体を処理するクラスです。
public class KeyTextController : MonoBehaviour
{
    // 変数
    //  コンポーネント変数
    //  コンポーネント
    Rigidbody rigidbody;
    //  内部
    public bool isEnter = false;
    private TextMeshPro textMeshPro;
    float timeToLive = 2.0f;
    private float currentTime;
    private Vector3 size;
    private Color color;
    private float speed = 0.16f;
    private float convertMilitoUnitWeight = 1f;


    void Start()
    {
        // 初期取得
        rigidbody = GetComponent<Rigidbody>();
        textMeshPro = this.GetComponent<TextMeshPro>();
        size = gameObject.transform.localScale;
        color = textMeshPro.color;

        // 初期設定
        currentTime = 0;
        speed += Random.value * 0.05f;
    }

    void Update()
    {
        KeyText();
    }

    void KeyText()
    {
        if (currentTime < timeToLive)
        {
            // 上昇
            Vector3 position = rigidbody.position;
            position.y += speed * convertMilitoUnitWeight * Time.deltaTime;
            rigidbody.MovePosition(position);

            // エンター時でない場合は縮小
            if (!isEnter)
            {
                gameObject.transform.localScale = size * (timeToLive - currentTime) / timeToLive;
            }
            // エンターの場合は透明化
            else
            {
                textMeshPro.color = new Color(color.r, color.g, color.b, 1 * (timeToLive - currentTime) / timeToLive);
            }

            currentTime += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}