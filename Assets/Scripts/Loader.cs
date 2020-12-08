using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public static Loader instance;

    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }

    public void LaunchScene(int sceneIndex)
    {
        if (sceneIndex > 1)
        {
            Screen.orientation = ScreenOrientation.Landscape;
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator  LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}
