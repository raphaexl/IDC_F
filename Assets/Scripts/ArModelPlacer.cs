using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ArModelPlacer : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private GameObject visual;
    private ARCameraManager aRCamera;

    public GameObject prefab;
    private GameObject viewer;

    private bool placed = false;
    private bool foundPlane = false;
    [SerializeField] Button cameraBtn;

    // Start is called before the first frame update

    void  Awake()
    {
        cameraBtn.onClick.AddListener(() =>
        {
            CameraReset();
        });
    }

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        aRCamera = FindObjectOfType<ARCameraManager>();
        visual = transform.GetChild(0).gameObject;
        visual.SetActive(false);
        viewer = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        viewer.SetActive(false);
    }

    void CheckPlanes()
    {
        if (placed)
            return;
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
            visual.SetActive(true);
            foundPlane = true;
        }
    }

    void PlaceModel()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (!placed)
            {
                if (foundPlane)
                {
                    viewer.transform.position = transform.position;
                    viewer.transform.rotation = transform.rotation;
                    // viewer.transform.LookAt(aRCamera.transform);
                    viewer.SetActive(true);
                    visual.SetActive(false);
                    placed = true;
                    cameraBtn.gameObject.SetActive(true);
                    ModelViewer.SetModel(viewer);
                }
            }

        }
    }

    public void CameraReset()
    {
        {
            viewer.SetActive(false);
            foundPlane = false;
            placed = false;
            cameraBtn.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlanes();
        PlaceModel();
    }
}
