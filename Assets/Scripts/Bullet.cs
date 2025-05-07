using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject, 0);
    }
}
