using UnityEngine;

namespace DoorScript
{
    [RequireComponent(typeof(AudioSource))]
    public class Door : MonoBehaviour
    {
        public bool open;
        public float smooth = 1.0f;
        public float DoorOpenAngle = -120.0f;
        public float DoorCloseAngle = 0.0f;

        //Allows the door to stay where you put it at the start
        private float currentTargetAngle;

        public AudioSource asource;
        public AudioClip openDoor, closeDoor;

        void Start()
        {
            asource = GetComponent<AudioSource>();

            //Slightly open, set the target to its current rotation
            currentTargetAngle = transform.localEulerAngles.y;

            // If you manually rotated it in the Inspector, ensure its open that way
            if (Mathf.Abs(currentTargetAngle - DoorCloseAngle) > 5f)
            {
                // Door is already partially open
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

        public void OpenDoor()
        {
            open = !open;

            currentTargetAngle = open ? DoorOpenAngle : DoorCloseAngle;

            asource.clip = open ? openDoor : closeDoor;
            asource.Play();
        }
    }
}