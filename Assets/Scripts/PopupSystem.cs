using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSystem : MonoBehaviour
{
    public static PopupSystem current;
    public PopupWindow window;

    private void Awake()
    {
        current = this;
    }

    public static void Show(string content, string header = "")
    {
       /* current.window.gameObject.transform.DOScale(0.5f, 0.1f).OnComplete(() =>
        {
            transform.DOScale(1.5f, 0.5f).OnComplete(() =>
            {
                transform.DOScale(1f, 0.2f);
            });
        });*/
        current.window.SetText(content, header);
        current.window.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.window.gameObject.SetActive(false);
    }
}
