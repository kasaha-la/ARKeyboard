using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyController : MonoBehaviour
{
    Rigidbody rigidbody;
    public float timeToLive;
    private float currentTime;
    private Vector3 size;

    // Start is called before the first frame update
    void Start()
    {
        // rigidbody = GetComponent<Rigidbody>();
        size = gameObject.transform.localScale; 
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime < timeToLive)
        {
            gameObject.transform.localScale = size * ((timeToLive-currentTime) / timeToLive);
            currentTime += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
