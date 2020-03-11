using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusButtonController : MonoBehaviour
{
    private bool status = false;
    GameObject ARSessionOrigin;
    Text text;

    void Start()
    {
        ARSessionOrigin = GameObject.Find("AR Session Origin");
        text =  gameObject.GetComponentInChildren<Text>();
        text.text=status.ToString();
    }

    void Update()
    {
        
    }

    public void OnClick(){
        status = !status;
        Debug.Log(setStatusText(status));
        text.text=setStatusText(status);
        if(status){
            ARSessionOrigin.GetComponent<MainProcess>().StopObjectTracking();
        }else{
            ARSessionOrigin.GetComponent<MainProcess>().StartObjectTracking();
        }
        
    }

    string setStatusText(bool status){
        return status?"static":"tracking";
    }
}
