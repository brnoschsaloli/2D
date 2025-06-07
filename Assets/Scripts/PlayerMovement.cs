using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpForce = 5f;
    public float joystickJumpForce = 5f;
    public float fallMultiplier = 2f;
    public float lowJumpMultiplier = 2f;
    public float fastFallMultiplier = 3f;
    public float joystickLowJumpMultiplier = 1.5f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool isRunning;
    private AudioSource audioSource;
    private PlayerLifeStats lifeStats;
    private bool isAttacking = false;

    public int lookDirection = 1;
    [SerializeField] private AudioClip shoot;
    public AudioSource sfxAudioSource;

    [Header("Mobile Input")]
    public Joystick joystick;

    private bool joystickJumpPressed = false;
    private bool joystickJumpConsumed = false;
    private bool wasJoystickUp = false;

    [Header("UI Buttons")]
    public Button shootButton; 


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        lifeStats = GetComponent<PlayerLifeStats>();
        if (shootButton != null)
            shootButton.onClick.AddListener(Attack);
    }


    void Update()
    {
        // ── Death check ──
        if (lifeStats != null && lifeStats.currentHearts <= 0)
        {
            animator.SetBool("isDead", true);
            Debug.Log("Player isDead!");
            enabled = false;
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
            Collider2D col = GetComponent<Collider2D>();
            if (col) col.enabled = false;
            return;
        }

        // ── Keyboard actions ──
        if (Input.GetKeyDown(KeyCode.Space))
            Attack();
        if (Input.GetKeyDown(KeyCode.Q))
            SceneManager.LoadScene("Menu");

        // ── Read inputs ──
        float joystickHorizontal = joystick != null ? joystick.Horizontal : 0f;
        float joystickVertical = joystick != null ? joystick.Vertical : 0f;
        float moveInput = Input.GetAxisRaw("Horizontal") + joystickHorizontal;

        // ── Horizontal movement & facing ──
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
            lookDirection = 1;
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(
                -Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
            lookDirection = -1;
        }
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // ── Running animation & sound ──
        bool wasRunning = isRunning;
        isRunning = Mathf.Abs(moveInput) > 0.1f;
        animator.SetBool("isRunning", isRunning);

        if (isRunning && !wasRunning && isGrounded)      audioSource.Play();
        else if (!isRunning && wasRunning)               audioSource.Stop();

        // ── Jump logic ──
        bool keyboardJump = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
        bool joystickJump = joystick != null && joystick.Vertical > 0.5f && !joystickJumpConsumed;

        if ((keyboardJump || joystickJump) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            if (joystickJump)
                joystickJumpConsumed = true;
        }

        // Reset joystick jump when stick is released
        if (joystick != null && joystick.Vertical <= 0.3f)
        {
            joystickJumpConsumed = false;
        }

        // ── Better‐jump gravity ──
        bool jumpHeld = Input.GetKey(KeyCode.UpArrow)
                    || Input.GetKey(KeyCode.W)
                    || (joystick != null && joystick.Vertical > 0.5f);

        if (rb.linearVelocity.y < 0f)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.linearVelocity.y > 0f && !jumpHeld)
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }

        // ── Fast‐fall ──
        bool fastFallInput = (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                        || (joystick != null && joystickVertical < -0.5f);

        if (fastFallInput && !isGrounded)
        {
            rb.gravityScale = fastFallMultiplier;
        }
    }


    void Attack()
    {
        if (isAttacking) return;
        isAttacking = true;
        animator.SetBool("isAttacking", true);
        rb.linearVelocity = Vector2.zero;
    }

    public void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            joystickJumpConsumed = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;


    private void Shoot()
    {
        sfxAudioSource.PlayOneShot(shoot);
        GameSession.Instance.AddCoin();
        Debug.Log("Atirou   : " + lookDirection);
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (lookDirection != 1 && lookDirection != -1)
        {
            lookDirection = 1;
        }
        bullet.SetDirection(lookDirection);
    }
}