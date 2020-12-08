using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class VideoController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    VideoPlayer videoPlayer;
    [SerializeField] private Button playBtn;
    [SerializeField]  private Button stepFwdBtn;
    [SerializeField] private Button stepBwdBtn;
    [SerializeField]  private Slider slider;

    [SerializeField] private Text currentTime;
    [SerializeField] private Text durationTime;

    [SerializeField] private Sprite pauseImg;
    [SerializeField] private Sprite playImg;
    [SerializeField] private  string videoName;

    float controllerTimer = 5f;

    bool isDone;

    public bool IsPlaying
    {
        get { return videoPlayer.isPlaying; }
    }

    public bool IsLooping
    {
        get { return videoPlayer.isLooping; }
    }

    public bool IsPrepared
    {
        get { return videoPlayer.isPrepared; }
    }

    public bool IsDone
    {
        get { return isDone; }
    }

    public double Time
    {
        get { return videoPlayer.time; }
    }

    public ulong Duration
    {
        get { return (ulong)(videoPlayer.frameCount / videoPlayer.frameRate); }
    }

    public double Ntime
    {
        get { return Time / Duration; }
    }

    private void OnEnable()
    {
        videoPlayer.errorReceived += errorReceived;
        videoPlayer.frameReady += frameReady;
        videoPlayer.loopPointReached += loopPointReached;
        videoPlayer.prepareCompleted += prepareCompleted;
        videoPlayer.seekCompleted += seekCompleted;
        videoPlayer.started += started;
    }

    private void OnDisable()
    {
        videoPlayer.errorReceived -= errorReceived;
        videoPlayer.frameReady -= frameReady;
        videoPlayer.loopPointReached -= loopPointReached;
        videoPlayer.prepareCompleted -= prepareCompleted;
        videoPlayer.seekCompleted -= seekCompleted;
        videoPlayer.started -= started;
    }

    public void OnPointerEnter(PointerEventData ped)
    {
       /* if (IsPlaying)
        {
            this.gameObject.SetActive(true);
            StartCoroutine(ExcecuteAfterTime(controllerTimer));
            this.gameObject.SetActive(false);
        }*/
    }


    public void OnPointerExit(PointerEventData ped)
    {
       /* if (IsPlaying)
        {
            this.gameObject.SetActive(false);
        }*/
    }


    IEnumerator ExcecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
    }

    void errorReceived(VideoPlayer v, string msg)
    {
        Debug.Log("Video Player Error : " + msg);
    }

    void frameReady(VideoPlayer v, long frame){}

    void loopPointReached(VideoPlayer v)
    {
        Debug.Log("Video Player Loop Point Reached !");
        isDone = true;
    }

    void prepareCompleted(VideoPlayer v)
    {
        Debug.Log("Video Player finished Preparing !");
        isDone = false;
    }

    void seekCompleted(VideoPlayer v)
    {
        Debug.Log("Video Player finished Seeking !");
        isDone = false;
    }

    void started(VideoPlayer v)
    {
        Debug.Log("Video Player started !");
    }


    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        playImg = playBtn.GetComponent<Image>().sprite;

        playBtn.onClick.AddListener(() =>
        {
            PlayVideo();
            ToggleButton();
        });

        stepFwdBtn.onClick.AddListener(() =>
        {
            IncrementPlayBackSpeed();
        });

        stepBwdBtn.onClick.AddListener(() =>
        {
            DecrementPlayBackSpeed();
        });
        LoadVideo(videoName);
    }

    public void Prepare()
    {
        videoPlayer.Prepare();
        playBtn.GetComponent<Image>().sprite = playImg;
    }



    private void ToggleButton()
    {
        if (videoPlayer.isPlaying)
        {
            playBtn.GetComponent<Image>().sprite = pauseImg;
        }
        else
        {
            playBtn.GetComponent<Image>().sprite = playImg;
        }
    }

    public void LoadVideo(string name)
    {
        string temp = Application.dataPath + "/Videos/" + name;
        if (videoPlayer.url == temp)
        {
            return;
        }
        videoPlayer.url = temp;
        videoPlayer.loopPointReached += EndReahed;
        videoPlayer.Prepare();

        Debug.Log("Video can set direct audio Volume " + videoPlayer.canSetDirectAudioVolume);
        Debug.Log("Video can set PlayBack speed " + videoPlayer.canSetPlaybackSpeed);
        Debug.Log("Video can skip on drop " + videoPlayer.canSetSkipOnDrop);
        Debug.Log("Video can set time " + videoPlayer.canSetTime);
        Debug.Log("Video can step " + videoPlayer.canStep);
        //videoPlayer.StepForward();
    }


    private void EndReahed(VideoPlayer vp)
    {
        vp.playbackSpeed = vp.playbackSpeed / 10.0F;
    }

    public void PlayVideo()
    {
        if (!IsPrepared)
        {
            return;
        }
        if(videoPlayer.isPlaying)
             videoPlayer.Pause();
         else
        {
            videoPlayer.Play();
        }
    }

    public void PauseVideo()
    {
        if (!IsPlaying)
        {
            return;
        }
        videoPlayer.Pause();
    }

    public void Stop()
    {
        slider.value = 0f;
        Seek(0);
        videoPlayer.time = 0f;
        videoPlayer.Stop();
    }

    public void RestartVideo()
    {
        if (!IsPrepared)
        {
            return;
        }
        PauseVideo();
        Seek(0);
    }

    public void LoopVideo(bool toggle)
    {
        if (!IsPrepared)
        {
            return;
        }
        videoPlayer.isLooping = toggle;
    }

    public void Seek(float nTime)
    {
        if (!IsPrepared || !videoPlayer.canSetTime)
        {
            return;
        }
        nTime = Mathf.Clamp01(nTime);
        videoPlayer.time = nTime * Duration;
    }

    public void IncrementPlayBackSpeed()
    {
        if (!videoPlayer.canSetPlaybackSpeed)
        {
            return;
        }
        videoPlayer.playbackSpeed += 1;
    }

    public void DecrementPlayBackSpeed()
    {
        if (!videoPlayer.canSetPlaybackSpeed)
        {
            return;
        }
        videoPlayer.playbackSpeed -= 1;
    }

    private void Update()
    {
        slider.value = (float)Ntime;
        //Debug.Log("Current Time : " + Time + " Floor :  " + Mathf.Floor((float)Time) + " Ceil : " + Mathf.Ceil((float)Time));
        //Debug.Log("Total Duration Time : " +  Duration);
        SetUITime();
    }

    void SetUITime()
    {
        currentTime.text = SetTimeString((float)Time);
        durationTime.text = SetTimeString((float)(Duration - Time) < 0 ? 0 : (float)(Duration - Time));
    }

    string  SetTimeString(float time, bool isLong=false)
    {
        float hf = time / 3600f;
        int hours = (int)Mathf.Floor(hf);
        float mf = (hf - hours) * 60f;
        int minutes = (int)Mathf.Floor(mf);
        int seconds = (int)Mathf.Floor((mf - minutes) * 60);
        string msg = "";
        if (isLong)
        {
            if (hours < 10)
            {
                msg += "0";
            }
            msg += hours.ToString();
            msg += ":";
        }
        if (minutes < 10)
        {
            msg += "0";
        }
        msg += minutes.ToString();
        msg += ":";
        if (seconds < 10)
        {
            msg += "0";
        }
        msg += seconds.ToString();
        return msg;
    }
}
