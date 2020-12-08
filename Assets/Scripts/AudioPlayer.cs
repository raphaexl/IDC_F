using UnityEngine;
using UnityEngine.UI;

public class AudioPlayer : MonoBehaviour
{

    private Button playBtn;
    private Button stopBtn;
    private Slider slider;
    private Sprite playImg;
    [SerializeField] private Sprite pauseImg;

    AudioSource audio;
    public AudioClip clip;

    // Start is called before the first frame update
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
        audio = gameObject.AddComponent<AudioSource>();
        AudioPlayerInit();
    }

    void AudioPlayerInit()
    {
        slider.minValue = 0f;
        slider.maxValue = clip.length - 1f;

        slider.onValueChanged.AddListener(delegate
        {
            MoveSlider(slider);
        });
        audio.clip = clip;
    }

    private void ToggleButton()
    {
        if (audio.isPlaying)
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
        if (audio.isPlaying)
            audio.Pause();
        else
        {
            audio.Play();
        }
    }

    private void Stop()
    {
        audio.Stop();
    }

    public void ChangeClip()
    {
        if (audio.isPlaying)
            audio.Stop();
        else
            AudioPlayerInit();
    }

    private void MoveSlider(Slider slider)
    {
        audio.time = slider.value;
        if (slider.value < 0.01f)
        {
            playBtn.GetComponent<Image>().sprite = playImg;
        }
    }

    private void Update()
    {
        slider.value = audio.time;
    }
}
