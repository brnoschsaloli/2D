using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpForce = 5f;
    public float fallMultiplier = 2f;
    public float lowJumpMultiplier = 2f;
    public float fastFallMultiplier = 3f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool isRunning;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Movimento horizontal
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Animação de corrida
        bool wasRunning = isRunning;
        isRunning = Mathf.Abs(moveInput) > 0.1f;
        animator.SetBool("isRunning", isRunning);

        // Som de corrida
        if (isRunning && !wasRunning && isGrounded)
        {
            audioSource.Play();
        }
        else if (!isRunning && wasRunning)
        {
            audioSource.Stop();
        }

        // Pulo
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Ajustes de gravidade para subida e queda
        if (rb.linearVelocity.y < 0)
        {
            // Caindo
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            // Subindo mas botão de pulo solto
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            // Gravidade normal
            rb.gravityScale = 1f;
        }

        // Queda rápida com tecla S ou seta para baixo
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !isGrounded)
        {
            rb.gravityScale = fastFallMultiplier;
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