using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if we hit an obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject); // remove obstacle
            Destroy(gameObject);           // remove projectile
        }
        else
        {
            // Optional: destroy projectile after 2 seconds if it hits anything else
            Destroy(gameObject, 2f);
        }
    }
}
