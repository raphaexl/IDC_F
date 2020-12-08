using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressedAudio : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData ped)
    {
        FindObjectOfType<AudioManager>().PlayOneShot("ButtonPressed");
    }

    public void OnPointerUp(PointerEventData ped)
    {

    }
}