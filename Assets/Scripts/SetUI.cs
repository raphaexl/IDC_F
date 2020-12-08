using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUI : MonoBehaviour
{
    public GameObject UIitem;
    public Vector3 offset;

    public Camera cm;
   

    // Update is called once per frame
    void Update()
    {
        Vector3 newpos = cm.WorldToScreenPoint(transform.position + offset);
        if(newpos != UIitem.transform.position)
            UIitem.transform.position = newpos;
    }
}
