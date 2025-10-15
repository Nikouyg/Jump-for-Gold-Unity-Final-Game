using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30;
    private PlayerController playerControllerScript;
    private float leftBound = -15;

    void Start()
    {
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
            playerControllerScript = playerObj.GetComponent<PlayerController>();
    }

    void Update()
    {
        // Stop moving if the game is over
        if (playerControllerScript != null && playerControllerScript.gameOver)
            return;

        // Move the background/obstacles left to simulate movement
        transform.Translate(Vector3.left * Time.deltaTime * speed);

        // Destroy obstacles that go off-screen
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
