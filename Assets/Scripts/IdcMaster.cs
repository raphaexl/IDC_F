using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdcMaster : MonoBehaviour
{
    [SerializeField] Transform[] positions;
    [SerializeField] Transition transition;



    IdcMessages[] toHallMessages;
    IdcMessages[] toRoomMessages;
    IdcMessages[] roomMessages;

    
   

    [SerializeField] AudioClip[] audioClips;

    private void Awake()
    {
        Globals.currentLocation = Globals.Location.HALL;
        Globals.nextLocation = Globals.Location.HALL;
        InitIDCMessages();
    }

    public void UpdatePositon()
    {
        transition.ChangeLocation(positions[(int)Globals.nextLocation]);
    }

    void InitIDCMessages()
    {
        toHallMessages = new IdcMessages[5];
        toRoomMessages = new IdcMessages[5];
        roomMessages = new IdcMessages[5];

        roomMessages[0] = new IdcMessages("COULOIR", "Le Couloir de L'IDC");
        roomMessages[1] = new IdcMessages("ROOM 1", "La SALLE 1 de L'IDC");
        roomMessages[2] = new IdcMessages("ROOM 2", "La SALLE 2 de L'IDC");
        roomMessages[3] = new IdcMessages("ROOM 3", "La SALLE 3 de L'IDC");
        roomMessages[4] = new IdcMessages("ROOM 4", "La SALLE 4 de L'IDC");
        
        toHallMessages[0] = new IdcMessages("","");
        toHallMessages[1] = new IdcMessages("DOOR", " Revenir au COULOIR");
        toHallMessages[2] = new IdcMessages("DOOR", " Revenir au COULOIR");
        toHallMessages[3] = new IdcMessages("DOOR", " Revenir au COULOIR");
        toHallMessages[4] = new IdcMessages("DOOR", " Revenir au COULOIR");

        toRoomMessages[0] = new IdcMessages("", "");
        toRoomMessages[1] = new IdcMessages("DOOR", " Entrez dans la SALLE 1");
        toRoomMessages[2] = new IdcMessages("DOOR", " Entrez dans la SALLE 2");
        toRoomMessages[3] = new IdcMessages("DOOR", " Entrez dans la SALLE 3");
        toRoomMessages[4] = new IdcMessages("DOOR", " Entrez dans la SALLE 4");
    }

    public IdcMessages DisplayIDCMsg()
    {
        IdcMessages msg;
        Debug.Log("Current Location : " + Globals.currentLocation + " Next Location : " + Globals.nextLocation);
        if (Globals.isGazing)
        {
            if (Globals.currentLocation == Globals.Location.HALL)
            {
                msg = toRoomMessages[(int)Globals.nextLocation];
            }
            else
            {
                msg = toHallMessages[(int)Globals.currentLocation];
            }
        }
        else
        {
            msg = roomMessages[(int)Globals.currentLocation];
        }
        return msg;
    }
}
