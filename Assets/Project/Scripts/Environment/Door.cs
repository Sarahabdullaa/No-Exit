using UnityEngine;

namespace DoorScript
{
    [RequireComponent(typeof(AudioSource))]
    public class Door : MonoBehaviour, IInteractable
    {
        public bool open;
        public float smooth = 1.0f;
        public float DoorOpenAngle = -120.0f;
        public float DoorCloseAngle = 0.0f;
        public ForestDoor forestDoor;

        public GameObject endingCanvas;
        public MonoBehaviour playerController;
        public MonoBehaviour mouseLook;

        //Allows the door to stay where you put it at the start
        private float currentTargetAngle;

        public AudioSource asource;
        public AudioClip openDoor, closeDoor;

        [Header("Locking")]
        public string requiredPuzzle;   // "clock", "lamp", "star" or empty for no requirement
        public AudioClip lockedSound;

        void Start()
        {
            asource = GetComponent<AudioSource>();

            //Slightly open set the target to its current rotation
            currentTargetAngle = transform.localEulerAngles.y;

            // if manually rotated in the Inspector ensure its open
            if (Mathf.Abs(currentTargetAngle - DoorCloseAngle) > 5f)
            {
            }
        }

        void Update()
        {
            float targetY = open ? DoorOpenAngle : DoorCloseAngle;

            if (!open && currentTargetAngle != DoorCloseAngle)
            {
                targetY = currentTargetAngle;
            }

            Quaternion targetRotation = Quaternion.Euler(0, targetY, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * 5 * smooth);
        }

        public void Interact()
        {

            // Check if door is locked
            if (!string.IsNullOrEmpty(requiredPuzzle))
            {
                bool unlocked = false;
                switch (requiredPuzzle.ToLower())
                {
                    case "clock": unlocked = PuzzleProgress.ClockCompleted; break;
                    case "lamp": unlocked = PuzzleProgress.LampCompleted; break;
                    case "star": unlocked = PuzzleProgress.StarCompleted; break;
                }
                if (!unlocked)
                {
                    if (lockedSound != null) asource.PlayOneShot(lockedSound);
                    Debug.Log($"Door to room {requiredPuzzle} is locked!");
                    return;
                }
            }
            OpenDoor();
        }

        public void OpenDoor()
        {
            open = !open;

            currentTargetAngle = open ? DoorOpenAngle : DoorCloseAngle;

            asource.clip = open ? openDoor : closeDoor;
            asource.Play();

            if (open)
            {
                if (forestDoor != null)
                {
                    forestDoor.OpenDoor();
                }

                if (endingCanvas != null)
                {
                    endingCanvas.SetActive(true);

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    if (playerController != null)
                        playerController.enabled = false;

                    if (mouseLook != null)
                        mouseLook.enabled = false;
                }
            }
        }

    }
}