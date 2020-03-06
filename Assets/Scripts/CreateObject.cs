using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARTrackedImageManager))]
public class CreateObject : MonoBehaviour
{
    [SerializeField]
    GameObject objectPrefab;
    GameObject spawnedObject;

    ARRaycastManager raycastManager;
    ARTrackedImageManager m_TrackedImageManager;
    ARTrackedObjectManager m_TrackedObjectManager;
    List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
        m_TrackedObjectManager = GetComponent<ARTrackedObjectManager>();
    }

    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        m_TrackedObjectManager.trackedObjectsChanged += OnTrackedObjectChanged;
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        m_TrackedObjectManager.trackedObjectsChanged -= OnTrackedObjectChanged;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            int iTCount = Input.touchCount;
            // DebugText.instance.SetValue("Touched : Count=" + iTCount);
            if (iTCount == 1)
            {
                // 生成 or 場所の変更
                Create(Input.GetTouch(0).position);

            }
            else if (iTCount == 2)
            {
                Rotate(Input.GetTouch(0));
                // 生成 or 回転
            }
            else
            {
            }
        }
    }

    // public Camera worldSpaceCanvasCamera;
    [SerializeField]
    Camera m_WorldSpaceCanvasCamera;
    public Camera worldSpaceCanvasCamera
    {
        get { return m_WorldSpaceCanvasCamera; }
        set { m_WorldSpaceCanvasCamera = value; }
    }


    /// <summary>
    /// If an image is detected but no source texture can be found,
    /// this texture is used instead.
    /// </summary>
    [SerializeField]
    Texture2D m_DefaultTexture;
    public Texture2D defaultTexture
    {
        get { return m_DefaultTexture; }
        set { m_DefaultTexture = value; }
    }

    void UpdateInfo(ARTrackedImage trackedImage)
    {
        // var canvas = trackedImage.GetComponent<Canvas>();
        // canvas.worldCamera = worldSpaceCanvasCamera;

        // // Update information about the tracked image

        // var text = canvas.GetComponentInChildren<Text>();
        // text.text = string.Format(
        //     "{0}\ntrackingState: {1}\nGUID: {2}\nReference size: {3} cm\nDetected size: {4} cm",
        //     trackedImage.referenceImage.name,
        //     trackedImage.trackingState,
        //     trackedImage.referenceImage.guid,
        //     trackedImage.referenceImage.size * 100f,
        //     trackedImage.size * 100f);

        // var planeParentGo = trackedImage.transform.GetChild(0).gameObject;
        // var planeGo = planeParentGo.transform.GetChild(0).gameObject;

        // // Disable the visual plane if it is not being tracked
        // if (trackedImage.trackingState != TrackingState.None)
        // {
        //     planeGo.SetActive(true);

        //     // The image extents is only valid when the image is being tracked
        //     trackedImage.transform.localScale = new Vector3(trackedImage.size.x, 1f, trackedImage.size.y);

        //     // Set the texture
        //     var material = planeGo.GetComponentInChildren<MeshRenderer>().material;
        //     material.mainTexture = (trackedImage.referenceImage.texture == null) ? defaultTexture : trackedImage.referenceImage.texture;
        // }
        // else
        // {
        //     planeGo.SetActive(false);
        // }
    }

    void UpdateObjInfo(ARTrackedObject trackedObject){
        // DebugText.instance.SetValue(trackedObject.transform.localPosition + " : " + trackedObject.transform.localRotation);
    }

    void Create(Vector2 v2TouchPos)
    {
        if (raycastManager.Raycast(v2TouchPos, hitResults, TrackableType.PlaneWithinPolygon))
        {
            // DebugText.instance.SetValue("Raycast detect");
            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(objectPrefab, hitResults[0].pose.position, hitResults[0].pose.rotation);
                // DebugText.instance.SetValue("Created " + spawnedObject.transform.position.x + ":" + spawnedObject.transform.position.y + ":" + spawnedObject.transform.position.z);
            }
            else
            {
                spawnedObject.transform.position = hitResults[0].pose.position;
                // DebugText.instance.SetValue("Replaced " + spawnedObject.transform.position.x + ":" + spawnedObject.transform.position.y + ":" + spawnedObject.transform.position.z);
            }
        }
    }

    void Rotate(Touch touch)
    {

    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            // Give the initial image a reasonable default scale
            // trackedImage.transform.localScale = new Vector3(0.01f, 1f, 0.01f);
            UpdateInfo(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
            UpdateInfo(trackedImage);
    }

    
    void OnTrackedObjectChanged(ARTrackedObjectsChangedEventArgs eventArgs)
    {
        foreach (var trackedObject in eventArgs.added)
        {
            // Give the initial image a reasonable default scale
            // trackedImage.transform.localScale = new Vector3(0.01f, 1f, 0.01f);
            UpdateObjInfo(trackedObject);
        }

        foreach (var trackedObject in eventArgs.updated)
            UpdateObjInfo(trackedObject);
    }
}