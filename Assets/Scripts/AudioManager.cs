using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    private Sound current = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = Globals.Instance.volume;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        current = s;
        s.source.Play();
    }

    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.PlayOneShot(s.clip, Globals.Instance.volume);
    }

    public void Stop()
    {
        if (current == null) return;
        else
        {
            current.source.Stop();
        }
    }

    public void Pause()
    {
        if (current == null) return;
        else
        {
            current.source.Pause();
        }
    }

    public void Continue()
    {
        if (current == null) return;
        else
        {
            current.source.Play();
        }
    }

    private void Update()
    {
        if (current != null)
        {
            current.source.volume = Globals.Instance.volume;
        }
  
    }
}
