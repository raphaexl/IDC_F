using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IcubeUiManager : MonoBehaviour
{
    [SerializeField] Button uiToggleBtn;
    [SerializeField] Button videoCloseBtn;
    [SerializeField] Button homeBtn;
   
    [SerializeField] Button infoBtn;
    [SerializeField] Button hideBtn;

    [SerializeField] GameObject videoPlayer;
    [SerializeField] GameObject menu;

   
    [SerializeField] VideoController videoController;

    private void Awake()
    {
        homeBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene((int)Scenes.MENU);
        });

        uiToggleBtn.onClick.AddListener(() =>
        {
            Show();
        });

        hideBtn.onClick.AddListener(() =>
        {
            Hide();
        });

        infoBtn.onClick.AddListener(() =>
        {
            VideoDisplay();
        });

        videoCloseBtn.onClick.AddListener(() =>
        {
            videoController.Stop();
            videoPlayer.SetActive(false);
            videoCloseBtn.gameObject.SetActive(false);
            menu.SetActive(true);
        });
    }

    public void Hide()
    {
        uiToggleBtn.gameObject.SetActive(true);
        menu.SetActive(false);
    }

    public void Show()
    {
        uiToggleBtn.gameObject.SetActive(false);
        menu.SetActive(true);
    }

    public void VideoDisplay()
    {
        uiToggleBtn.gameObject.SetActive(false);
        menu.SetActive(false);
        videoCloseBtn.gameObject.SetActive(true);
        videoPlayer.SetActive(true);
        videoController.Prepare();
    }

    
}
