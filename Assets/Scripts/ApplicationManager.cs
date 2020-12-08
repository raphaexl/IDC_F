using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApplicationManager : MonoBehaviour
{
    public static ApplicationManager instance;

    [SerializeField] Button close;
    [SerializeField] Button details;
    [SerializeField] Button detailsToMenu;
    [SerializeField] GameObject detailsGo;
    [SerializeField] GameObject mainMenuGo;

    [SerializeField] Slider volumeSlider;


    [SerializeField] Button idcTourBtn;
    [SerializeField] Button iCubeVrBtn;

    private void Awake()
    {
        instance = this;

        close.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        details.onClick.AddListener(() =>
        {
            mainMenuGo.SetActive(false);
            detailsGo.SetActive(true);
        });

        detailsToMenu.onClick.AddListener(() =>
        {
            detailsGo.SetActive(false);
            mainMenuGo.SetActive(true);
        });

        volumeSlider.onValueChanged.AddListener(delegate {
            Globals.Instance.volume = volumeSlider.value;
        });



        idcTourBtn.onClick.AddListener(() =>
        {
            FindObjectOfType<Loader>().LaunchScene((int)Scenes.VR_PHOTOS);
        });
        
        iCubeVrBtn.onClick.AddListener(() =>
        {
            FindObjectOfType<Loader>().LaunchScene((int)Scenes.VR_ICUBE);
        });
    }

    public void BackToScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
