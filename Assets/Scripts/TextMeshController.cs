using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 生成された文字自体を処理するクラスです。
public class TextMeshController : MonoBehaviour
{

    private TextMeshProUGUI textbox_name;
	public float timeToLive;
	private float currentTime;
    Rigidbody rigidbody;

    void Start()
    {
		rigidbody = GetComponent<Rigidbody>();
		textbox_name = GetComponent<TextMeshProUGUI>();
		currentTime = 0;
    }

    void Update()
    {
		if(currentTime < timeToLive){
			Vector3 position = rigidbody.position;
			position.y += 0.16f * Time.deltaTime;
			rigidbody.MovePosition(position);
			currentTime += Time.deltaTime;
		}else{
			Destroy(gameObject);
		}
    }
}