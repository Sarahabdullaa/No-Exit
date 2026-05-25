using UnityEngine;

public class BalloonFollow : MonoBehaviour
{
    private Transform playerTransform;

    public float rotationSpeed = 3f;

    void Start()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
         
            Vector3 directionToPlayer = playerTransform.position - transform.position;

      
            directionToPlayer.y = 0;

           
            if (directionToPlayer != Vector3.zero)
            {
               
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

                
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}