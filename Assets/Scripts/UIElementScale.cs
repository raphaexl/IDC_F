using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UIElementScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ 

    void Start()
    {

       transform.DOScale(0.5f, 0.1f).OnComplete(() =>
        {
            transform.DOScale(1.5f, 0.5f).OnComplete(() =>
            {
                transform.DOScale(1f, 0.2f);
            });
        });

    }

    private void OnEnable()
    {
        transform.DOScale(0.5f, 0.1f).OnComplete(() =>
        {
            transform.DOScale(1.5f, 0.5f).OnComplete(() =>
            {
                transform.DOScale(1f, 0.2f);
            });
        });
    }

    public void OnPointerEnter(PointerEventData ped)
    {
        transform.DOScale(1.5f, 0.5f);
    }

    public void OnPointerExit(PointerEventData ped)
    {
        transform.DOScale(1f, 0.5f);
    }
}
