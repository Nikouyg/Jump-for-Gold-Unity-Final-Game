using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    public float jumpForce = 10f;
    public float gravityModifier = 1f;
    public bool isOnGround = true;
    public bool gameOver = false;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    public GameObject projectilePrefab;
    public float throwForce = 500f;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -9.81f, 0) * gravityModifier;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        // Keep player locked on Z=0 for a proper side view
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }

    void Update()
    {
        if (gameOver) return;

        // Move left/right (side view)
        float horizontalInput = Input.GetAxis("Horizontal"); // ← / → or A / D keys
        float moveSpeed = 10f; // adjust as needed

        // Move player along X-axis only (no Z movement)
        Vector3 move = new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);
        transform.Translate(move, Space.World);

        // Flip player to face movement direction (no rotation into camera)
        if (horizontalInput > 0)
            transform.localScale = new Vector3(1, 1, 1);   // face right
        else if (horizontalInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);  // face left

        // Jump with Space
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
        }

        // Throw sphere with L
        if (Input.GetKeyDown(KeyCode.L))
        {
            // Create projectile slightly in front of player
            GameObject projectile = Instantiate(
                projectilePrefab,
                transform.position + Vector3.right * transform.localScale.x + Vector3.up * 1f,
                Quaternion.identity
            );

            Rigidbody projRb = projectile.GetComponent<Rigidbody>();
            projRb.AddForce(Vector3.right * transform.localScale.x * throwForce);
            Destroy(projectile, 3f);
        }

        // Clamp player position (keep in camera view)
        float minX = -20f;
        float maxX = 100f; // adjust to your level width
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            transform.position.y,
            0 // lock Z = 0 so player stays on side axis
        );
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerAudio.PlayOneShot(crashSound, 1.0f);
            Debug.Log("Game Over!");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
        }
    }
}
