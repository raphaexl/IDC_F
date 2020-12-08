using UnityEngine;
using UnityEngine.EventSystems;
public class ButtonHoverAudio : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    public void OnPointerEnter(PointerEventData ped)
    {
        FindObjectOfType<AudioManager>().PlayOneShot("ButtonHover");
    }

    public void OnPointerExit(PointerEventData ped)
    {
       // FindObjectOfType<AudioManager>().PlayOneShot("ButtonHover");
    }
}
