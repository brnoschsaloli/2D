using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float speed = 5f;
    private float direction = 1f;

    private Rigidbody2D rb;
    private Transform player;
    public void SetDirection(float dir)
    {
        direction = Mathf.Sign(dir); // Garante que será 1 ou -1
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > 20)
        {
            Destroy(gameObject, 0);
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage();
            }
            Destroy(gameObject, 0);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            Debug.Log("ta batendo no boss");
            BossHealth bossHealth = collision.gameObject.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage();
            }
            Destroy(gameObject, 0);
        }
        else
        {
            Debug.Log("ta batendo em algo" + collision.gameObject);
            Destroy(gameObject, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if we hit a boss or a boss part (like a claw hitbox)
        BossHealth bossHealth = other.GetComponentInParent<BossHealth>();
        if (bossHealth != null)
        {
            Debug.Log("Bullet hit boss via trigger (claw or body)");
            bossHealth.TakeDamage();
            Destroy(gameObject, 0);
            return;
        }
        // Optionally, handle other trigger logic here
    }
}

