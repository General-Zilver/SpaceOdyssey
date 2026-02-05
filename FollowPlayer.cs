using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerShip;       // The object we want to follow
    public Vector3 offset = new Vector3(0, 0, -10); // Keep camera "back" so it can see

    void LateUpdate()
    {
        // Check if the ship still exists (in case it gets destroyed later)
        if (playerShip != null)
        {
            // Teleport camera to Ship Position + Offset
            transform.position = playerShip.position + offset;
        }
    }
}