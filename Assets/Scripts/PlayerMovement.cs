using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpForce = 0.2f;
    public float fastFallMultiplier = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool isRunning;
    private AudioSource audioSource;  // Add AudioSource reference

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();  // Get the AudioSource component
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Movimento horizontal
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Define se está correndo (qualquer valor diferente de zero)
        bool wasRunning = isRunning;  // Store previous running state
        isRunning = Mathf.Abs(moveInput) > 0.1f;
        animator.SetBool("isRunning", isRunning);

        // Play sound when starting to run
        if (isRunning && !wasRunning && isGrounded)
        {
            audioSource.Play();
        }
        // Stop sound when stopping running
        else if (!isRunning && wasRunning)
        {
            audioSource.Stop();
        }

        // Pulo
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Queda rapida
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !isGrounded)
        {
            rb.gravityScale = fastFallMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}