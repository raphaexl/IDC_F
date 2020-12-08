using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewer : MonoBehaviour
{
    [SerializeField] GameObject scrolB;
    Scrollbar scrollBar;
    float[] positions;
    int posCount = 0;
    float distance;
    float scrollPos;

    // Start is called before the first frame update
    void Start()
    {
        positions = new float[transform.childCount];
        posCount = positions.Length;
        distance = 1.0f / (posCount - 1);
        scrollBar = scrolB.GetComponent<Scrollbar>();
    }

    // Update is called once per frame
    void Update()
    {        
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = distance * i;
        }
        if (Input.GetMouseButton(0))
        {
            scrollPos = scrollBar.value;
        }
       else
       {
           for (int i = 0; i < positions.Length; i++)
           {
               if ((scrollPos < positions[i] + (distance / 2f)) && (scrollPos > positions[i] - (distance / 2)))
               {
                   scrollBar.value = Mathf.Lerp(scrollBar.value, positions[i], 0.1f);
                }
           }
       }


       for (int i = 0; i < positions.Length; i++)
       {
           if ((scrollPos < positions[i] + (distance / 2)) && (scrollPos > positions[i] - (distance / 2f)))
           {
               transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, Vector2.one, 0.1f);
               for (int j = 0; j < positions.Length; j++)
               {
                   if (j != i)
                   {
                       transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                       transform.GetChild(j).GetComponent<Button>().interactable = false;
                   }
                   else
                   {
                       transform.GetChild(j).GetComponent<Button>().interactable = true;
                   }
               }
           }
       }
    }
}
