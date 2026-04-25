using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;



public class LoadingManager : MonoBehaviour
{
    public string sceneToLoad = "Hallway_Hub";
    public Slider loadingBar;

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        float displayedProgress = 0f;
        float speed = 0.5f; 

        while (!operation.isDone)
        {
            float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);

            // Smooth movement
            displayedProgress = Mathf.MoveTowards(displayedProgress, targetProgress, speed * Time.deltaTime);

            if (loadingBar != null)
                loadingBar.value = displayedProgress;

            if (operation.progress >= 0.9f && displayedProgress >= 0.99f)
            {
                yield return new WaitForSeconds(0.5f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}