using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ObjectDetails
{
    private Vector3 _position;
    private Quaternion _rotation;
    private Vector3 _scale;
    private Transform _objectTransform;
    private Outline _outline;
    
    public void GetInfo(Transform objectInfo,Outline outline)
    {
        _position = objectInfo.localPosition;
        _rotation = objectInfo.localRotation;
        _scale = objectInfo.localScale;
        _objectTransform = objectInfo;
        _outline = outline;
    }
    
    public void SetInfo()
    {
        _objectTransform.localPosition = _position;
        _objectTransform.localRotation = _rotation;
        _objectTransform.localScale = _scale;
        _outline.enabled = false;
    }
}

enum ObjectStates
{
    WholeIcube,AllObjects,IsolateObject
}

public class IcubeObjectsManager : MonoBehaviour
{
    [SerializeField] private Transform icubeObjectParent;

    [SerializeField] private Camera _camera;

    [SerializeField] private GameObject uiObjectCanvas;


    private ObjectDetails _objectDetails;
    private Vector3 _icubePos;
    private ObjectStates _objectStates;
    private bool _isDTouched;
    private GameObject icubeParent;

    void Start()
    {
        _objectDetails = new ObjectDetails();
        _objectStates = ObjectStates.WholeIcube;
        _isDTouched = false;
    }


    void Update()
    {
       
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);
            /*if (t.tapCount == 2 && !_isDTouched)
            {
                _isDTouched = true;
                SetMode();
            }
            else*/ if (t.tapCount == 1 && _objectStates == ObjectStates.AllObjects)
            {
                if (Mathf.Abs(t.deltaPosition.x) > 0.05f)
                {
                    Vector3 vmove = _camera.ScreenToViewportPoint(t.deltaPosition);
                    vmove.y = 0f;
                    _icubePos = icubeObjectParent.position;
                    _icubePos += vmove * 3f;
                    _icubePos.x = Mathf.Clamp(_icubePos.x, -3f, 0f);
                    icubeObjectParent.position = _icubePos;

                }
                else
                {
         
                    icubeParent = ModelViewer.GetModel();
                    GetObject(_camera.ScreenToViewportPoint(t.position));
                    _isDTouched = true;

                }
            }

            if (t.phase == TouchPhase.Ended)
                _isDTouched = false;
        }
    }

    public  void SetMode()
    {
        if (_objectStates == ObjectStates.WholeIcube)
        {
            _objectStates = ObjectStates.AllObjects;
            icubeObjectParent.gameObject.SetActive(true);
            uiObjectCanvas.SetActive(true);
            ModelViewer.SetActive(false);
        }
        else if (_objectStates == ObjectStates.AllObjects)
        {
            _objectStates = ObjectStates.WholeIcube;
            icubeObjectParent.gameObject.SetActive(false);
            uiObjectCanvas.SetActive(false);
            ModelViewer.SetActive(true);
        }
        else if (_objectStates == ObjectStates.IsolateObject)
        {
            _objectStates = ObjectStates.AllObjects;
            _objectDetails.SetInfo();
            EnableObject(icubeObjectParent);
            ModelViewer.SetModel(icubeParent);
            uiObjectCanvas.SetActive(true);
        }
    }

    void GetObject(Vector3 touchPosition)
    {
        Ray ray = _camera.ViewportPointToRay(touchPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var gm = hit.collider.gameObject;
            Outline outline = gm.GetComponent<Outline>();
            outline.enabled = true;
            _objectStates = ObjectStates.IsolateObject;
            ModelViewer.SetModel(hit.collider.gameObject);
            _objectStates = ObjectStates.IsolateObject;
            _objectDetails.GetInfo(gm.transform, outline);
            uiObjectCanvas.SetActive(false);
            DisableObject(icubeObjectParent.transform, gm.transform);
        }
    }

    void DisableObject(Transform parent, Transform child)
    {
        foreach (Transform tra in parent)
        {
            if (tra != child)
                tra.gameObject.SetActive(false);
        }
    }

    void EnableObject(Transform parent)
    {
        foreach (Transform tra in parent)
        {
            tra.gameObject.SetActive(true);
        }
    }
}
