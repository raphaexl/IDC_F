using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : Singleton<Globals>
{
    public  float volume = 0.7f;
    internal  bool isGazing = false;
    public enum Location
    {
        HALL = 0, IDC_ROOM_1, IDC_ROOM_2, IDC_ROOM_3, IDC_ROOM_4
    }
    public  Location currentLocation;
    public  Location nextLocation;
    public AudioClip currentClip;
}
