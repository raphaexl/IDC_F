using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LookAround : MonoBehaviour
{
    [SerializeField] GameObject playerBody;
    [SerializeField] ReticlePointer reticlePointer;

    private Gyroscope gyroscope;
    private bool gyroEnalbled = false;
    private Quaternion rot;

    float xRotation = 0f, yRotation =  0f;
    float mouseSensitivity = 100f;

    Vector2 lookInput;
    float inputDeadZone = 10f;

    Camera camera;
    RaycastHit hitObject;

   

    private void Start()
    {
        camera = FindObjectOfType<Camera>();
        inputDeadZone = Mathf.Pow((.1f * Screen.width) / inputDeadZone, 2);
        gyroEnalbled = EnableGyroscope();
       if (gyroEnalbled)
        {
            playerBody.transform.position = transform.position;
            transform.SetParent(playerBody.transform);
        }else if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            mouseSensitivity = 100f;
        }
        else
        {
            mouseSensitivity = 10f;
        }

        lookInput = Vector2.zero;
     }

    private bool  EnableGyroscope()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyroscope = Input.gyro;
            gyroscope.enabled = true;
            playerBody.transform.rotation = Quaternion.Euler(90, 90, 0);
            rot = new Quaternion(0, 0, 1, 0);
            return true;
        }
        return (false);
    }


    private void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation += mouseX;
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    private void TouchLook()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            switch (touch.phase)
            {
                case TouchPhase.Moved:
                    lookInput = touch.deltaPosition;
                    break;
                case TouchPhase.Canceled:
                case TouchPhase.Stationary:
                    lookInput = Vector2.zero;
                    break;
                default:break;
            }
        }
        if (lookInput.sqrMagnitude < inputDeadZone)
        {
            return;
        }

        lookInput *= mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation - lookInput.y, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.transform.Rotate(Vector3.up, lookInput.x);
    }

    

    void ReticlePointerUpdate()
    {
       // Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Ray ray = camera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hitObject, 25f))
        {
            reticlePointer.bigSize();
            UpdateLocationAndDoor(hitObject.collider.gameObject);
            Globals.Instance.isGazing = true;
        }
        else
        {
            reticlePointer.smallSize();
            Globals.Instance.isGazing = false;
        }
       // Debug.Log("Current Location : " + Globals.Instance.currentLocation + " Next Location : " + Globals.Instance.nextLocation);
    }

    private void Update()
    {
        if (gyroEnalbled)
        {
            transform.localRotation = gyroscope.attitude * rot;
        }else if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            MouseLook();
        }
        else
        {
            TouchLook();
        }
        ReticlePointerUpdate();
    }





    void UpdateLocationAndDoor(GameObject gameObject)
    {
        if (gameObject.name == "doorToRoom_1")
        {
            Globals.Instance.currentLocation = Globals.Location.HALL;
            Globals.Instance.nextLocation = Globals.Location.IDC_ROOM_1;
        }
        else if (gameObject.name == "doorToRoom_2")
        {
            Globals.Instance.currentLocation = Globals.Location.HALL;
            Globals.Instance.nextLocation = Globals.Location.IDC_ROOM_2;
        }
        else if (gameObject.name == "doorToRoom_3")
        {
            Globals.Instance.currentLocation = Globals.Location.HALL;
            Globals.Instance.nextLocation = Globals.Location.IDC_ROOM_3;
        }
        else if (gameObject.name == "doorToRoom_4")
        {
            Globals.Instance.currentLocation = Globals.Location.HALL;
            Globals.Instance.nextLocation = Globals.Location.IDC_ROOM_4;
        }
        else if (gameObject.name == "ExitRoom_1")
        {
            Globals.Instance.currentLocation = Globals.Location.IDC_ROOM_1;
            Globals.Instance.nextLocation = Globals.Location.HALL;
        }
        else if (gameObject.name == "ExitRoom_2")
        {
            Globals.Instance.currentLocation = Globals.Location.IDC_ROOM_2;
            Globals.Instance.nextLocation = Globals.Location.HALL;
        }
        else if (gameObject.name == "ExitRoom_3")
        {
            Globals.Instance.currentLocation = Globals.Location.IDC_ROOM_3;
            Globals.Instance.nextLocation = Globals.Location.HALL;
        }
        else if (gameObject.name == "ExitRoom_4")
        {
            Globals.Instance.currentLocation = Globals.Location.IDC_ROOM_4;
            Globals.Instance.nextLocation = Globals.Location.HALL;
        }
    }
}
