using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class batMovement : MonoBehaviour
{
    [Header("Velocidade de Voo")]
    [Tooltip("Velocidade vertical em unidades/segundo")]
    public float speed = 3f;

    private Rigidbody2D rb;
    private int moveDirection = 1; // +1 = sobe, -1 = desce

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // evita rotações indesejadas
    }

    void FixedUpdate()
    {
        // Mantém velocidade constante no eixo Y
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, moveDirection * speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 1) Dano imediato ao player, se houver PlayerStats no objeto colidido
        PlayerStats ps = collision.gameObject.GetComponent<PlayerStats>();
        if (ps != null)
        {
            ps.TakeDamage();
        }

        // 2) Inverte o sentido de voo ao bater no chão (“Ground”)
        if (collision.gameObject.CompareTag("Ground"))
        {
            moveDirection *= -1;
        }
    }
}
