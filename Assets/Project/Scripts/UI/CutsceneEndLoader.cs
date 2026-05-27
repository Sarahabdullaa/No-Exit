using UnityEngine;

public class CutsceneEndLoader : MonoBehaviour
{
    public float cutsceneDuration = 84f;

    public GameObject cutsceneRoot;

    public MonoBehaviour playerController;
    public MonoBehaviour mouseLook;
    public GameObject gameplayHUD;

    public Transform returnPoint;

    void Start()
    {
        Invoke(nameof(EndCutscene), cutsceneDuration);
    }

    void EndCutscene()
    {
        // Hide cutscene
        if (cutsceneRoot != null)
            cutsceneRoot.SetActive(false);

        // Move player
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && returnPoint != null)
        {
            player.transform.position = returnPoint.position;
            player.transform.rotation = returnPoint.rotation;
        }

        // Enable controls again
        if (playerController != null)
            playerController.enabled = true;

        if (mouseLook != null)
            mouseLook.enabled = true;

        if (gameplayHUD != null)
            gameplayHUD.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}