using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    public bool enableAutoScroll = false;


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
    }

void Update()
{
    if (gameOver) return;

    // Move left/right (side view)
    float horizontalInput = Input.GetAxis("Horizontal"); // ← / → or A / D keys
    float moveSpeed = 10f; // adjust as needed

    // Move the player along X-axis (side view)
    transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);

    // Optionally flip player to face movement direction
    if (horizontalInput > 0)
        transform.rotation = Quaternion.Euler(0, 90, 0);  // face right
    else if (horizontalInput < 0)
        transform.rotation = Quaternion.Euler(0, -90, 0); // face left

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
        GameObject projectile = Instantiate(
            projectilePrefab,
            transform.position + transform.forward + Vector3.up * 1f,
            Quaternion.identity
        );
        Rigidbody projRb = projectile.GetComponent<Rigidbody>();
        projRb.AddForce(transform.forward * throwForce);
        Destroy(projectile, 3f);
    }
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
