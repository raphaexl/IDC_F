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

    public IdcMessages msg;


    [SerializeField] AudioClip[] audioClips;

    private void Awake()
    {
        Globals.Instance.currentLocation = Globals.Location.HALL;
        Globals.Instance.nextLocation = Globals.Location.HALL;
        msg = new IdcMessages("", "");
        InitIDCMessages();
    }

    public void UpdatePositon()
    {
        transition.ChangeLocation(positions[(int)Globals.Instance.nextLocation]);
        
      //  Debug.Log("Is everything correct ?");
    }

    void InitIDCMessages()
    {
        toHallMessages = new IdcMessages[5];
        toRoomMessages = new IdcMessages[5];
        roomMessages = new IdcMessages[5];

        roomMessages[0] = new IdcMessages("", "Welcome To The IDC Moroco");
        roomMessages[1] = new IdcMessages("", "Learning Room N 1");
        roomMessages[2] = new IdcMessages("", "Learning Room N 1");
        roomMessages[3] = new IdcMessages("", "The EON Concave Room");
        roomMessages[4] = new IdcMessages("", "The Icube Room");
        
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

    void DisplayIDCMsg()
    {

        if (Globals.Instance.isGazing)
        {
            if (Globals.Instance.currentLocation == Globals.Location.HALL)
            {
                msg = toRoomMessages[(int)Globals.Instance.nextLocation];
            }
            else
            {
                msg = toHallMessages[(int)Globals.Instance.currentLocation];
            }
        }
        else
        {
            msg = roomMessages[(int)Globals.Instance.currentLocation];
        }
    }

    void Update()
    {
        DisplayIDCMsg();
    }
}
