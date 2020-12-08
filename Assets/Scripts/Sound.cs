
using UnityEngine;

[System.Serializable]
public class Sound
{
    [HideInInspector]
    public AudioSource source;
   // [HideInInspector]
    public AudioClip clip;

    public string name;
    private float volume;
    public bool loop;
}
