using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform cameraTransform;   // Drag your Main Camera here
    public float parallaxEffect = 0.5f; // Smaller = slower = farther away

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (cameraTransform == null) return;

        float distance = cameraTransform.position.x * parallaxEffect;
        transform.position = new Vector3(startPos.x + distance, startPos.y, startPos.z);
    }
}
