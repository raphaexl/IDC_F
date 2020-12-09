using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IcubeUiManager : MonoBehaviour
{
    [SerializeField] Button uiToggleBtn;
    [SerializeField] Button videoCloseBtn;
    [SerializeField] Button homeBtn;

    [SerializeField] private Button cameraBtn;
    [SerializeField] private TextMeshProUGUI waitMsg;
    [SerializeField] private TextMeshProUGUI placeMsg;

    [SerializeField] Button infoBtn;
    [SerializeField] Button hideBtn;

    [SerializeField] GameObject videoPlayer;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject toggleDissassembly = null;


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
            FindObjectOfType<AudioManager>().Pause();
            VideoDisplay();
        });

        videoCloseBtn.onClick.AddListener(() =>
        {
            videoController.Stop();
            videoPlayer.SetActive(false);
            videoCloseBtn.gameObject.SetActive(false);
            menu.SetActive(true);
            ModelViewer.SetActive(true);
            if (cameraBtn)
            { cameraBtn.gameObject.SetActive(true); }
            if (toggleDissassembly) { toggleDissassembly.SetActive(true); }
            FindObjectOfType<AudioManager>().Continue();
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
        menu.SetActive(false);
        ModelViewer.SetActive(false);
        if (cameraBtn && cameraBtn.gameObject.activeSelf)
        { cameraBtn.gameObject.SetActive(false); }
        if (waitMsg && waitMsg.gameObject.activeSelf)
        { waitMsg.gameObject.SetActive(false); }
        if (placeMsg && placeMsg.gameObject.activeSelf)
        { placeMsg.gameObject.SetActive(false); }
        if (toggleDissassembly) { toggleDissassembly.SetActive(false); }
        videoCloseBtn.gameObject.SetActive(true);
        videoPlayer.SetActive(true);
        videoController.Prepare();
        if (videoController.IsPrepared)
        {
            uiToggleBtn.gameObject.SetActive(false);
            
        }
    }

    
}
