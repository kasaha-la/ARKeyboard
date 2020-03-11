using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARTrackedImageManager))]
public class MainProcess : MonoBehaviour
{
    [SerializeField]
    GameObject objectPrefab;
    GameObject spawnedObject;

    ARRaycastManager raycastManager;
    ARTrackedObjectManager m_TrackedObjectManager;
    List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

    void Awake()
    {
        m_TrackedObjectManager = GetComponent<ARTrackedObjectManager>();
    }

    void OnEnable()
    {
        m_TrackedObjectManager.trackedObjectsChanged += OnTrackedObjectChanged;
    }

    void OnDisable()
    {
        m_TrackedObjectManager.trackedObjectsChanged -= OnTrackedObjectChanged;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            int iTCount = Input.touchCount;
            if (iTCount == 1)
            {
                // 生成 or 場所の変更
                // Create(Input.GetTouch(0).position);
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


    //認識した平面に手動で作成する場合の関数
    //使用数場合はAR Session OriginなどにAR Plane Managerをアタッチする必要がある
    // void Create(Vector2 v2TouchPos)
    // {
    //     if (raycastManager.Raycast(v2TouchPos, hitResults, TrackableType.PlaneWithinPolygon))
    //     {
    //         if (spawnedObject == null)
    //         {
    //             spawnedObject = Instantiate(objectPrefab, hitResults[0].pose.position, hitResults[0].pose.rotation);
    //         }
    //         else
    //         {
    //             spawnedObject.transform.position = hitResults[0].pose.position;
    //         }
    //     }
    // }


    //オブジェクトトラッキングが更新（新規も含む）された際の処理
    void OnTrackedObjectChanged(ARTrackedObjectsChangedEventArgs eventArgs)
    {
        foreach (var trackedObject in eventArgs.added)
        {
            UpdateObjInfo(trackedObject);
        }

        foreach (var trackedObject in eventArgs.updated)
            UpdateObjInfo(trackedObject);
    }

    private ARTrackedObject staticObject;

    void UpdateObjInfo(ARTrackedObject trackedObject)
    {
        trackedObject.gameObject.SetActive(true);
    }

    // トラッキングを止めて、位置を固定する
    public void StopObjectTracking()
    {
        m_TrackedObjectManager.enabled = false;
    }

    // トラッキングを再開する　※再開時に現在のオブジェクトは一旦ディアクティブする。
    public void StartObjectTracking()
    {
        foreach (ARTrackedObject obj in m_TrackedObjectManager.trackables)
        {
            obj.gameObject.SetActive(false);
        }
        m_TrackedObjectManager.enabled = true;
    }
}