using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ForestDoor : MonoBehaviour
{
    public string Room5SceneName = "Room_5";
    private bool loaded = false;

    public void OpenDoor()
    {
        if (!loaded)
        {
            StartCoroutine(LoadForest());
            loaded = true;
        }
    }

    IEnumerator LoadForest()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(Room5SceneName, LoadSceneMode.Additive);

        while (!op.isDone)
        {
            yield return null;
        }

        // Apply forest lighting properly
        DynamicGI.UpdateEnvironment();
    }
}