using UnityEngine;
using UnityEngine.UI;

public class AudioPlayer : MonoBehaviour
{

    private Button playBtn;
    private Button stopBtn;
    private Slider slider;
    private Button volumeBtn;
    private Slider volumeSlider;
    private Sprite playImg;
    [SerializeField] private Sprite pauseImg;

    AudioSource audio;
    [HideInInspector] public AudioClip clip;

    // Start is called before the first frame update
    void Awake()
    {
        playBtn = transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Button>();
        stopBtn = transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Button>();
        slider = transform.GetChild(0).gameObject.transform.transform.GetChild(2).GetComponent<Slider>();

        volumeBtn = transform.GetChild(1).gameObject.transform.GetChild(1).GetComponent<Button>();
        volumeSlider = transform.GetChild(1).transform.GetChild(0).GetComponent<Slider>();
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

        volumeBtn.onClick.AddListener(() =>
        {
            if (volumeSlider.gameObject.activeSelf)
            { volumeSlider.gameObject.SetActive(false); }
            else
            { volumeSlider.gameObject.SetActive(true); }
        });
        audio = gameObject.AddComponent<AudioSource>();

    }

    private void OnEnable()
    {
        UpdateClip();
    }

    private void OnDisable()
    {
        UpdateClip();
    }
   
    void AudioPlayerInit()
    {
        slider.minValue = 0f;
        slider.maxValue = clip.length - 0.1f;

        slider.onValueChanged.AddListener(delegate
        {
            MoveSlider(slider);
        });
        volumeSlider.minValue = 0f;
        volumeSlider.maxValue = 0.9f;

        volumeSlider.onValueChanged.AddListener(delegate
        {
            MoveVolumeSlider(volumeSlider);
        });
        volumeSlider.value = 0.7f;
        volumeSlider.gameObject.SetActive(false);
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

    public void UpdateClip()
    {
        if (!audio)
        {
            return ;
        }
        if (audio.isPlaying)
            audio.Stop();
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

    private void MoveVolumeSlider(Slider slider)
    {
        audio.volume = slider.value;
    }

    private void Update()
    {
        slider.value = audio.time;
        volumeSlider.value = audio.volume;
    }
}
