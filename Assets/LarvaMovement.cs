using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class LarvaMovement : MonoBehaviour
{
    [Header("Configura��o de Movimento")]
    [Tooltip("Velocidade horizontal em unidades/segundo")]
    public float speed = 2f;

    private Rigidbody2D rb;
    private int moveDirection = 1; // +1 = para a direita, -1 = para a esquerda

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Garantir que o Rigidbody n�o gire com colis�es
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        // Aplica velocidade horizontal constante
        rb.linearVelocity = new Vector2(moveDirection * speed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // S� reagir a colis�es com objetos marcados como "Ground"
        if (!collision.gameObject.CompareTag("larvaWall"))
            return;

        // Verificar se a colis�o foi lateral (normal aponta para x)
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (Mathf.Abs(contact.normal.x) > 0.5f)
            {
                // Inverte dire��o
                moveDirection *= -1;

                // Espelha o sprite virando a escala em X
                Vector3 s = transform.localScale;
                s.x = Mathf.Sign(moveDirection) * Mathf.Abs(s.x);
                transform.localScale = s;
                break;
            }
        }
    }
}
