using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VrIcubeManager : MonoBehaviour
{
    [SerializeField] Button arBtn;
    [SerializeField] Button vrBtn;
    [SerializeField] Button dissBtn;
    [SerializeField] Button assBtn;

    [SerializeField] GameObject icube;

    void Awake()
    {
        vrBtn.interactable = false;
        arBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene((int)Scenes.AR_ICUBE);
        });

        dissBtn.onClick.AddListener(() =>
        {
            FindObjectOfType<IcubeObjectsManager>().SetMode();
            dissBtn.gameObject.SetActive(false);
            assBtn.gameObject.SetActive(true);
        });

        assBtn.onClick.AddListener(() =>
        {
            FindObjectOfType<IcubeObjectsManager>().SetMode();
            assBtn.gameObject.SetActive(false);
            dissBtn.gameObject.SetActive(true);
        });
    }
    // Start is called before the first frame update
    void Start()
    {
        ModelViewer.SetModel(Instantiate(icube));
    }

    
}
