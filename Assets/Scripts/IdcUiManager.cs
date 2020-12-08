
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class IdcUiManager : MonoBehaviour
{
    [SerializeField] Button homeBtn;
    [SerializeField] Button infoBtn;
    [SerializeField] Button backInfoBtn;
    [SerializeField] Button audioShowBtn;
    [SerializeField] Button audioHideBtn;

    [SerializeField] Button telePortBtn;

    [SerializeField] GameObject audioPlayer;

    private void Awake()
    {
        homeBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene((int)Scenes.MENU);
        });

        audioShowBtn.onClick.AddListener(() =>
        {
            ShowAudio();
        });

        audioHideBtn.onClick.AddListener(() =>
        {
            HideAudio();
        });

        infoBtn.onClick.AddListener(() =>
        {
            IdcMessages msg = FindObjectOfType<IdcMaster>().DisplayIDCMsg();
            PopupSystem.Show(msg.Content, msg.Header);
            
            backInfoBtn.gameObject.SetActive(true);
            infoBtn.gameObject.SetActive(false);
            telePortBtn.gameObject.SetActive(Globals.isGazing);
        });

        backInfoBtn.onClick.AddListener(() =>
        {
            PopupSystem.Hide();
            telePortBtn.gameObject.SetActive(false);
            backInfoBtn.gameObject.SetActive(false);
            infoBtn.gameObject.SetActive(true);
        });

        telePortBtn.onClick.AddListener(() =>
        {        
            telePortBtn.gameObject.SetActive(false);
            backInfoBtn.gameObject.SetActive(false);
            infoBtn.gameObject.SetActive(true);
            PopupSystem.Hide();
            FindObjectOfType<IdcMaster>().UpdatePositon();
            //if (audioPlayer.is)
            //audioPlayer.clip = audioClips[(int)Globals.nextLocation];
            //FindObjectOfType<AudioPlayer>().ChangeClip();
        });


    }

    void ShowAudio()
    {
        audioPlayer.SetActive(true);
        audioHideBtn.gameObject.SetActive(true);
        audioShowBtn.gameObject.SetActive(false);
    }

    void HideAudio()
    {
        audioPlayer.SetActive(false);
        audioHideBtn.gameObject.SetActive(false);
        audioShowBtn.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Globals.isGazing)
        {
            if (!infoBtn.GetComponent<ButtonAnim>().enabled)
                infoBtn.GetComponent<ButtonAnim>().enabled = true;
        }
        else
        {
            infoBtn.GetComponent<ButtonAnim>().Stop();
            infoBtn.GetComponent<ButtonAnim>().enabled = false;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            IdcMessages msg = FindObjectOfType<IdcMaster>().DisplayIDCMsg();
            PopupSystem.Show(msg.Content, msg.Header);

            backInfoBtn.gameObject.SetActive(true);
            infoBtn.gameObject.SetActive(false);
            telePortBtn.gameObject.SetActive(Globals.isGazing);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            PopupSystem.Hide();
            telePortBtn.gameObject.SetActive(false);
            backInfoBtn.gameObject.SetActive(false);
            infoBtn.gameObject.SetActive(true);
        }
    }
}
