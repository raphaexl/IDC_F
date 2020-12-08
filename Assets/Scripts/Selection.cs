using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Selection : MonoBehaviour
{
    private static Camera camera;
    [HideInInspector]
    public static GameObject selectedObj;
    private static Renderer selectedObjRenderer;
    private static RaycastHit hitObject;

    [SerializeField] private static Material selectedNewMat;
    private static Material selectedOldMat;


    void Start()
    {
        camera = FindObjectOfType<Camera>();
        selectedObj = null;
    }

   

    // Update is called once per frame
    public static bool FireRays(Touch touch)
    {
         Ray ray = camera.ScreenPointToRay(touch.position);

        if (Physics.Raycast(ray, out hitObject))
        {
            //if (selectedObj)
            //{
            //    Debug.Log("Child Destroyed");
            //    DestroyImmediate(selectedObj);
            //}
            if (selectedObj != hitObject.collider.gameObject)
            {
                /*if (selectedObj != null)
                {
                    selectedObjRenderer.material = selectedOldMat;
                }*/
                selectedObj = hitObject.collider.gameObject;
                //  selectedObjRenderer = selectedObj.GetComponent<Renderer>();
                //selectedOldMat = selectedObjRenderer.material;
                //selectedObjRenderer.material = selectedNewMat;
                return true;
            }
        }
        //else
        //{
        //    if (selectedObj != null)
        //    {
        //      //  selectedObjRenderer.material = selectedOldMat;
        //        selectedObj = null;
        //    }
        //}
        return false;
    }

}
