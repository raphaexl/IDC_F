
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

    [SerializeField] IdcMaster idcMaster;

    private void Awake()
    {
        homeBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene((int)Scenes.MENU);
        });

        audioShowBtn.onClick.AddListener(() =>
        {
            ShowAudio();
            FindObjectOfType<AudioManager>().Pause();
        });

        audioHideBtn.onClick.AddListener(() =>
        {
            HideAudio();
            FindObjectOfType<AudioManager>().Continue();
        });

        infoBtn.onClick.AddListener(() =>
        {
            PopupSystem.Show(idcMaster.msg.Content, idcMaster.msg.Header);
            
            backInfoBtn.gameObject.SetActive(true);
            infoBtn.gameObject.SetActive(false);
            telePortBtn.gameObject.SetActive(Globals.Instance.isGazing);
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
            idcMaster.UpdatePositon();
            //audioPlayer.SetActive(false);
        });


    }

    void ShowAudio()
    {
        audioPlayer.SetActive(true);
       // audioPlayer.transform.GetChild(1).gameObject.transform.GetChild(0).transform.gameObject.SetActive(false);
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
        if (Globals.Instance.isGazing)
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
            IdcMessages msg = FindObjectOfType<IdcMaster>().msg;
            PopupSystem.Show(msg.Content, msg.Header);

            backInfoBtn.gameObject.SetActive(true);
            infoBtn.gameObject.SetActive(false);
            telePortBtn.gameObject.SetActive(Globals.Instance.isGazing);
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
