using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class SpiderMovement : MonoBehaviour
{
    [Header("Configura��o de Movimento")]
    [Tooltip("Velocidade horizontal em unidades/segundo")]
    public float speed = 2f;
    public PlayerStats playerStats;

    [Header("Configura��o de Dano")]
    [Tooltip("Intervalo, em segundos, entre danos quando o player est� em contato")]
    public float damageInterval = 1.5f;

    private Rigidbody2D rb;
    private int moveDirection = 1; // +1 = para a direita, -1 = para a esquerda

    // Para controlar a coroutine de dano
    private Coroutine damageCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection * speed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Se colidiu com o player, d� dano imediato e inicia o DOT
        PlayerStats ps = collision.gameObject.GetComponent<PlayerStats>();
        if (ps != null)
        {
            ps.TakeDamage();
            if (damageCoroutine == null)
                damageCoroutine = StartCoroutine(DamageOverTime(ps));
        }

        // Se colidiu com a parede da spider, inverte dire��o
        if (!collision.gameObject.CompareTag("spiderWall"))
            return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (Mathf.Abs(contact.normal.x) > 0.5f)
            {
                moveDirection *= -1;
                Vector3 s = transform.localScale;
                s.x = Mathf.Sign(moveDirection) * Mathf.Abs(s.x);
                transform.localScale = s;
                break;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Para de dar dano quando o player sair do contato
        PlayerStats ps = collision.gameObject.GetComponent<PlayerStats>();
        if (ps != null && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    private IEnumerator DamageOverTime(PlayerStats ps)
    {
        // Enquanto o player continuar em contato, a coroutine vai loopar
        while (true)
        {
            yield return new WaitForSeconds(damageInterval);
            ps.TakeDamage();
        }
    }
}
