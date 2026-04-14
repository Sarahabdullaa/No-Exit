using UnityEngine;

public class DrawerOpen : MonoBehaviour
{

    public Vector3 closedPos;
    public Vector3 openPos;
    public float speed = 5f;
    private bool isOpen = false;

    void Start()
    {
        closedPos = transform.localPosition;
        openPos = closedPos + new Vector3(0, 0, 0.4f); // adjust axis here
    }

    void Update()
    {
        Vector3 target = isOpen ? openPos : closedPos;
        transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * speed);
    }

    public void ToggleDrawer()
    {
        isOpen = !isOpen;
    }
}
