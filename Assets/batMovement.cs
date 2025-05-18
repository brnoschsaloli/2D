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
        rb.freezeRotation = true; // evita rota��es indesejadas
    }

    void FixedUpdate()
    {
        // Mant�m velocidade constante no eixo Y
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, moveDirection * speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Se colidir com algo taggeado como "Ground", inverte o sentido
        if (collision.gameObject.CompareTag("Ground"))
        {
            moveDirection *= -1;
        }
    }
}
