using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 生成された文字自体を処理するクラスです。
public class TextMeshController : MonoBehaviour
{

    private TextMeshPro textbox_name;
    public float timeToLive;
    private float currentTime;
    private Vector3 size;
    public bool isEnter = false;
    Rigidbody rigidbody;
    private Color color;
    private float speed = 0.16f;
#if UNITY_EDITOR
    private float convertMilitoUnitWeight = 1f;
#else
        private float convertMilitoUnitWeight = 1f;
#endif

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        textbox_name = this.GetComponent<TextMeshPro>();
        size = gameObject.transform.localScale;
        color = textbox_name.color;
        currentTime = 0;

        speed += Random.value * 0.05f;
    }

    void Update()
    {
        if (currentTime < timeToLive)
        {
            Vector3 position = rigidbody.position;
            position.y += speed * convertMilitoUnitWeight * Time.deltaTime;
            rigidbody.MovePosition(position);
            currentTime += Time.deltaTime;
            if (!isEnter)
                gameObject.transform.localScale = size * (timeToLive - currentTime) / timeToLive;
            else
            {
                textbox_name.color = new Color(color.r, color.g, color.b, 1 * (timeToLive - currentTime) / timeToLive);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}