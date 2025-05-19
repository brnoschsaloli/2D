using UnityEngine;
using UnityEngine.SceneManagement;
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
    private PlayerLifeStats lifeStats;
    private bool isAttacking = false;

    public int lookDirection = 1;
    [SerializeField] private AudioClip shoot;
    public AudioSource sfxAudioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        lifeStats = GetComponent<PlayerLifeStats>();
    }

    void Update()
    {
        if (lifeStats != null && lifeStats.currentHearts <= 0)
        {
            animator.SetBool("isDead", true);
            Debug.Log("Player isDead!");
            enabled = false;
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static;
            }
            var col = GetComponent<Collider2D>();
            if (col != null) col.enabled = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("Menu");
        }
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            lookDirection = 1;
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            lookDirection = -1;
        }
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        bool wasRunning = isRunning;
        isRunning = Mathf.Abs(moveInput) > 0.1f;
        animator.SetBool("isRunning", isRunning);

        if (isRunning && !wasRunning && isGrounded)
        {
            audioSource.Play();
        }
        else if (!isRunning && wasRunning)
        {
            audioSource.Stop();
        }

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.linearVelocity.y > 0 && !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }

        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !isGrounded)
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