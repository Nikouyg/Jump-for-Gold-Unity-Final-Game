using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;      // drag your Player here in Inspector
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0, 2, -10); // adjust to fit your scene

    void LateUpdate()
    {
        if (player == null) return;

        // Follow only on X, keep Y and Z steady
        Vector3 targetPosition = new Vector3(player.position.x + offset.x, offset.y, offset.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
