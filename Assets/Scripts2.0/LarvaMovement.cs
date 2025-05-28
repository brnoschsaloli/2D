using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class LarvaMovement : MonoBehaviour
{
    [Header("Configura��o de Movimento")]
    [Tooltip("Velocidade horizontal em unidades/segundo")]
    public float speed = 2f;

    [Header("Configura��o de Dano")]
    [Tooltip("Intervalo, em segundos, entre danos quando o player est� em contato")]
    public float damageInterval = 1.5f;

    private Rigidbody2D rb;
    private int moveDirection = 1; // +1 = para a direita, -1 = para a esquerda
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
        // Se colidir com o player, aplica dano imediato e inicia a coroutine
        PlayerStats ps = collision.gameObject.GetComponent<PlayerStats>();
        if (ps != null)
        {
            ps.TakeDamage();
            if (damageCoroutine == null)
                damageCoroutine = StartCoroutine(DamageOverTime(ps));
        }

        // L�gica de invers�o de dire��o ao bater na parede da larva
        if (!collision.gameObject.CompareTag("larvaWall"))
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
        // Para de causar dano quando o player se afasta
        PlayerStats ps = collision.gameObject.GetComponent<PlayerStats>();
        if (ps != null && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    private IEnumerator DamageOverTime(PlayerStats ps)
    {
        while (true)
        {
            yield return new WaitForSeconds(damageInterval);
            ps.TakeDamage();
        }
    }
}
