using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text textDisplay;

    [TextArea]
    public string[] lines;

    public float typingSpeed = 0.04f;
    public float lineDelay = 2f;

    void Start()
    {
        StartCoroutine(PlayLines());
    }

    IEnumerator PlayLines()
    {
        foreach (string line in lines)
        {
            textDisplay.text = "";

            foreach (char letter in line)
            {
                textDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

            yield return new WaitForSeconds(lineDelay);
        }
    }
}