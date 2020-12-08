using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonAnim : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    Color lerpColor;
    private Image img;
    Color lightColor;
    Color hardColor;
    bool pressed = false;

    private void Awake()
    {
        img = GetComponent<Image>();
        /*lightBLue = new Color(0, 0, 0.75f, 0.7f);
         hardBLue = new Color(0, 0, 1f, 0.9f);*/
        lightColor = new Color(img.color.r, img.color.g, img.color.b, 0.5f);
        hardColor = new Color(img.color.r, img.color.g, img.color.b, 1f);
    }

    public void OnPointerDown(PointerEventData ped)
    {
        pressed = true;
    }

    public void OnPointerUp(PointerEventData ped)
    {
        pressed = false;
    }

    public void Stop()
    {
        img.color = hardColor;
    }

    private void Update()
    {
        if (!pressed)
        {
            lerpColor = Color.Lerp(lightColor, hardColor, Mathf.PingPong(Time.time, 0.2f));
            img.color = lerpColor;
        }
    }
}
