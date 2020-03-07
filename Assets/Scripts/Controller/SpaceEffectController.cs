using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceEffectController : MonoBehaviour
{
    // 変数
    //  コンポーネント変数
    //  コンポーネント
    private MeshRenderer mesh;
    //  内部
    private float timeToLive;
    private float currentTime;
    private Color color;
    private Vector3 size;
    private float speed = 0.26f;
    private float convertMilitoUnitWeight = 1f;

    void Start()
    {
        size = gameObject.transform.localScale;
        mesh = GetComponent<MeshRenderer>();
        color = mesh.material.color;
        currentTime = 0;
        timeToLive = 0.7f;

        speed += Random.value * 0.05f;
    }

    void Update()
    {
        if (currentTime < timeToLive)
        {
            Vector3 position = gameObject.transform.position;
            position.y += speed * convertMilitoUnitWeight * Time.deltaTime;
            gameObject.transform.position = position;
            currentTime += Time.deltaTime;
            Vector3 nowSize = new Vector3(size.x, size.y * (timeToLive - currentTime) / timeToLive, size.z);
            gameObject.transform.localScale = nowSize;

            mesh.material.color = new Color(color.r, color.g, color.b, 1 * (timeToLive - currentTime) / timeToLive);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
