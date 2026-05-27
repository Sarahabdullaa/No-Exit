using UnityEngine;
using System.Collections;

public class ReturnFromCutscene : MonoBehaviour
{
    public Transform returnPoint;

    IEnumerator Start()
    {
        // Restore puzzle progress
        if (PlayerPrefs.GetInt("MoldCompleted", 0) == 1)
        {
            PuzzleProgress.MoldCompleted = true;
        }

        // Wait one frame so player fully spawns first
        yield return null;

        if (PlayerPrefs.GetInt("ReturnFromCutscene", 0) == 1)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null && returnPoint != null)
            {
                player.transform.position = returnPoint.position;
                player.transform.rotation = returnPoint.rotation;
            }

            PlayerPrefs.SetInt("ReturnFromCutscene", 0);
        }
    }
}