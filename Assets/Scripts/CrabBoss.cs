using UnityEngine;

public class CrabBoss : MonoBehaviour
{
    [Header("Player & Arena")]
    public Transform player;
    public Transform arenaTrigger; // Optional: set if you want a defined zone

    [Header("Activation Delay")]
    public float followDelay = 2f;
    private bool playerEntered = false;
    private float delayTimer;

    [Header("Movement")]
    public float normalSpeed = 1f;
    public float speedBoost = 2f;
    public float speedBoostDistance = 3f;

    [Header("Behavior Distances")]
    public float chaseRange = 10f;

    [Header("Attack")]
    public float attackRange = 1.9f;
    public float attackCooldown = 2f;
    private float attackTimer = 0f;
    private bool isAttacking = false;
    private readonly string[] attackAnimations = { "Attack1", "Attack2", "Attack3", "Special" };

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private bool isChasing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        delayTimer = followDelay;
    }

    void Update()
    {
        float horizontalDistance = Mathf.Abs(transform.position.x - player.position.x);

        bool inAttackRange = horizontalDistance <= attackRange;
        if (inAttackRange)
        {
            if (!isAttacking && attackTimer <= 0f)
            {
                StartAttack();
            }
            animator.SetBool("IsWalking", false);
            movement = Vector2.zero;
        }
        else
        {
            isAttacking = false;
        }

        if (attackTimer > 0f)
            attackTimer -= Time.deltaTime;

        // Activate when player is within arena
        if (!playerEntered && horizontalDistance <= chaseRange)
        {
            playerEntered = true;
            delayTimer = followDelay;
        }

        // Wait the delay before chasing
        if (playerEntered)
        {
            if (delayTimer > 0)
            {
                delayTimer -= Time.deltaTime;
                animator.SetBool("IsWalking", false); // still idle
                return;
            }

            // Begin chase
            if (horizontalDistance <= chaseRange && horizontalDistance > attackRange)
            {
                isChasing = true;
                animator.SetBool("IsWalking", true);
                // Modify movement to only use X direction
                Vector2 directionToPlayer = (player.position - transform.position).normalized;
                movement = new Vector2(directionToPlayer.x, 0).normalized;

                // Flip sprite based on direction
                if (movement.x < 0)
                    transform.localScale = new Vector3(-1, 1, 1);
                else
                    transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontalDistance > chaseRange)
            {
                isChasing = false;
                animator.SetBool("IsWalking", false);
                movement = Vector2.zero;
            }
        }
    }

    void FixedUpdate()
    {
        // Prevent movement during attack
        float horizontalDistance = Mathf.Abs(transform.position.x - player.position.x);
        if (isChasing && !isAttacking)
        {
            float currentSpeed = horizontalDistance < speedBoostDistance
                ? speedBoost
                : normalSpeed;

            rb.linearVelocity = new Vector2(movement.x * currentSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    void StartAttack()
    {
        isAttacking = true;
        attackTimer = attackCooldown;

        int randomIndex = Random.Range(0, attackAnimations.Length);
        string attackAnim = attackAnimations[randomIndex];
        animator.Play(attackAnim);
    }

    public GameObject attackHitbox; // Assign in Inspector

    public void EnableHitbox()
    {   
        // Debug.Log("EnableHitbox() triggered by animation");
        attackHitbox.SetActive(true);
    }

    public void DisableHitbox()
    {
        // Debug.Log("DisableHitbox() triggered by animation");
        attackHitbox.SetActive(false);
    }


    // Called from Animation Event
    public void EndAttack()
    {
        // Debug.Log("EndAttack() triggered by animation");
        isAttacking = false;

        float horizontalDistance = Mathf.Abs(transform.position.x - player.position.x);
        if (horizontalDistance <= chaseRange && horizontalDistance > attackRange)
        {
            animator.SetBool("IsWalking", true);
            animator.Play("Walk");
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.Play("Idle Animation"); // Make sure this is the exact state name
        }
    }
}
