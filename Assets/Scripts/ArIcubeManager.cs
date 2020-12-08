using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArIcubeManager : MonoBehaviour
{
    [SerializeField] Button arBtn;
    [SerializeField] Button vrBtn;


    void Awake()
    {
        arBtn.interactable = false;
        vrBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene((int)Scenes.VR_ICUBE);
        });
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
