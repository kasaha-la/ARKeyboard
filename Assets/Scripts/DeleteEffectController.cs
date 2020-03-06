using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEffectController : MonoBehaviour
{
    
    private float timeToLive = 0.3f;
    private float currentTime = 0;
    private Vector3 size;
    // Start is called before the first frame update
    void Awake()
    {
        size = new Vector3(KeyboardLayout.body.sizeX/1000,0.001f,KeyboardLayout.body.sizeZ/1000);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime<timeToLive){
            Vector3 nowSize = size;
            Vector3 nowPos = gameObject.transform.localPosition;
            float timeRasio = (timeToLive - currentTime) / timeToLive;
            nowSize.x = (((KeyboardLayout.body.sizeX) - Mathf.Abs((timeToLive/2 - currentTime)/(timeToLive/2)) * (KeyboardLayout.body.sizeX))*0.001f)/2;
            gameObject.transform.localScale =  nowSize ;
            nowPos.x = (((KeyboardLayout.body.sizeX*timeRasio) - KeyboardLayout.body.sizeX/2) * 0.001f);
            gameObject.transform.localPosition = nowPos;
            currentTime += Time.deltaTime;
        }else{
            Destroy(gameObject);
        }
    }
}
