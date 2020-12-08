using UnityEngine;
using UnityEngine.UI;

public class ReticlePointer : MonoBehaviour
{
    [SerializeField] float MIN_SIZE = 0.75f;
    [SerializeField] float MAX_SIZE = 1.5f;
    static ReticlePointer instance;
    private  Image image;
    private  Vector2 imgSize;

    

    public void Start()
    {
        image = GetComponent<Image>();
        imgSize = image.rectTransform.sizeDelta;
    }

    public  void smallSize()
    {
        image.rectTransform.sizeDelta = imgSize * MIN_SIZE;
        ///image.rectTransform.sizeDelta = 100 * Vector2.one;
    }
    public void bigSize()
    {
        image.rectTransform.sizeDelta = imgSize * MAX_SIZE;
        // image.rectTransform.sizeDelta = 150 * Vector2.one;
        // Debug.Log("Zoom in Please");
    }
}
