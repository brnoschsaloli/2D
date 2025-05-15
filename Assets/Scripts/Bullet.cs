using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private Transform player;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > 20)
        {
            Destroy(gameObject, 0);
        }

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject, 0);
    }
}
