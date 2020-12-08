using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    GameObject fader;
    GameObject player;

    public float TRANSITION_TIME = 0.75f;


    private void Awake()
    {
        fader = GameObject.Find("Fader");
        player = GameObject.Find("Player");
        if (fader == null)
        {
            Debug.LogWarning("You Forgot to specify a fader on the camera ");
        }
        
    }

    public void ChangeLocation(Transform nextLocation)
    {
        StartCoroutine(FadeCamera(nextLocation));
    }

    IEnumerator FadeCamera(Transform nextLocation)
    {
        if (fader != null)
        {
            StartCoroutine(FadeIn(TRANSITION_TIME, fader.GetComponent<Renderer>().material));
            yield return new WaitForSeconds(TRANSITION_TIME);
            player.transform.position = nextLocation.position;
            if (Globals.Instance.currentLocation != Globals.Location.HALL)
                Globals.Instance.nextLocation = Globals.Location.HALL;
            Globals.Instance.currentLocation = Globals.Instance.nextLocation;
            StartCoroutine(FadeOut(TRANSITION_TIME, fader.GetComponent<Renderer>().material));
            yield return new WaitForSeconds(TRANSITION_TIME);
        }
        else
        {
            player.transform.position = nextLocation.position;
        }
    }

    IEnumerator FadeIn(float time, Material m)
    {
        while (m.color.a < 1.0f)
        {
            m.color = new Color(m.color.r, m.color.g, m.color.b, m.color.a + Time.deltaTime / time);
            yield return null;
        }
    }

    IEnumerator FadeOut(float time, Material m)
    {
        while (m.color.a > 0.1f)
        {
            m.color = new Color(m.color.r, m.color.g, m.color.b, m.color.a - Time.deltaTime / time);
            yield return null;
        }
    }
}
