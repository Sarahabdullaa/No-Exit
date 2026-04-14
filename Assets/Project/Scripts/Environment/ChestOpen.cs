using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    public Transform lid;
    public float openAngle = -90f;
    public float speed = 2f;

    private bool isOpen = false;

    void Update()
    {
        if (isOpen)
        {
            Quaternion targetRotation =
                Quaternion.Euler(openAngle, 0, 0);

            lid.localRotation = Quaternion.Slerp(
                lid.localRotation,
                targetRotation,
                Time.deltaTime * speed);
        }
    }

    public void OpenChest()
    {
        isOpen = true;
    }
}