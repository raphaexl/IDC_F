using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    private Button playBtn;
    private Button stopBtn;
    private Slider slider;
    private Sprite playImg;
    [SerializeField] private Sprite pauseImg;
    public VideoClip clip;
    private VideoPlayer videoPlayer;
    bool isSliding = false;

    void Awake()
     {
         playBtn = transform.GetChild(0).gameObject.GetComponent<Button>();
         stopBtn = transform.GetChild(1).gameObject.GetComponent<Button>();
         slider = transform.GetChild(2).gameObject.GetComponent<Slider>();
        
         playImg = playBtn.GetComponent<Image>().sprite;

        playBtn.onClick.AddListener(() =>
         {
             Play();
             ToggleButton();
         });

         stopBtn.onClick.AddListener(() =>
         {
             Stop();
         });

         videoPlayer = gameObject.GetComponent<VideoPlayer>();
         videoPlayer.clip = clip;
         VideoPlayerInit();
     }

     void VideoPlayerInit()
     {
          slider.minValue = 0f;
        slider.maxValue = (float)(clip.frameCount - 1) / (float)clip.frameCount;
        isSliding = false;
         slider.onValueChanged.AddListener(delegate
         {
             MoveSlider(slider);
         });
         videoPlayer.clip = clip;
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

     private void Play()
     {
         if (videoPlayer.isPlaying)
             videoPlayer.Pause();
         else
         {
             videoPlayer.Play();
         }
     }

     private void Stop()
     {
        slider.value = 0f;
         videoPlayer.Stop();
     }

     public void ChangeClip()
     {
         if (videoPlayer.isPlaying)
             videoPlayer.Stop();
         else
             VideoPlayerInit();
     }

     private void MoveSlider(Slider slider)
     {
        float frame = slider.value * (float)videoPlayer.frameCount;
        isSliding = true;
        videoPlayer.frame = (long)(frame);
        if (slider.value < 0.01f)
        {
            playBtn.GetComponent<Image>().sprite = playImg;
        }
        else
        {

        }
     }

     private void Update()
     {
        if (!isSliding)
        {
            slider.value = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
        }
     }
}
