using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    private AsyncOperation loadOperation;

    public void PreloadScene(string sceneName)
    {
        loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        loadOperation.allowSceneActivation = false;
    }

    public IEnumerator ActivateSceneAfterAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (loadOperation != null)
            loadOperation.allowSceneActivation = true;
    }
}