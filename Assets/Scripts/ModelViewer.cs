using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelViewer : MonoBehaviour
{
    public static ModelViewer instance;
    public GameObject model;

    [SerializeField] float speed;
    Vector2 rotSpeed;

    float scaleSpeed = 0.1f;
    float scaleMinBound = 0.1f;
    float scaleMaxBound = 50.0f;

    private float lerpSpeed = .1f;

    private void Awake()
    {
        instance = this;
    }

    void Scale(float delta, float speed)
    {
        model.transform.localScale -= Vector3.one * delta * speed * Time.deltaTime;
        model.transform.localScale = new Vector3(Mathf.Clamp(model.transform.localScale.x, scaleMinBound, scaleMaxBound),
            Mathf.Clamp(model.transform.localScale.y, scaleMinBound, scaleMaxBound),
            Mathf.Clamp(model.transform.localScale.z, scaleMinBound, scaleMaxBound));
    }

    void processTouchInput()
    {
        if (Input.touchCount == 2)
        {
            Touch tZero = Input.GetTouch(0);
            Touch tOne = Input.GetTouch(1);
            Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
            Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;
            float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
            float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);
            float deltaDistance = oldTouchDistance - currentTouchDistance;
            Scale(deltaDistance, scaleSpeed);
        }
        else if (Input.touchCount == 1)
        {

            Touch touch = Input.GetTouch(0);
            {
                Vector2 touchPrevious = touch.position - touch.deltaPosition;
                rotSpeed = touch.position - touchPrevious;

                float damp = lerpSpeed;
                rotSpeed = Vector3.Lerp(rotSpeed, Vector3.zero, damp);
                model.transform.RotateAround(Vector3.up, -rotSpeed.x * speed * Time.deltaTime);
                model.transform.RotateAround(Vector3.right, rotSpeed.y * speed * Time.deltaTime);
            }
        }
    }

    void processDesktopInput()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        speed = 10f;
        rotSpeed = new Vector2(mouseX, mouseY);
        float damp = lerpSpeed;
        rotSpeed = Vector3.Lerp(rotSpeed, Vector3.zero, damp);
        model.transform.RotateAround(Vector3.up, -rotSpeed.x * speed * Time.deltaTime);
        model.transform.RotateAround(Vector3.right, rotSpeed.y * speed * Time.deltaTime);

    }


    private void Update()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            processDesktopInput();
        }
        else
        {
            processTouchInput();
        }
    }
}
