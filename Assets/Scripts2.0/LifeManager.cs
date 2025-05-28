using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour
{
    [SerializeField]
    private int vidas = 3;

    private Animator animator;

    void Awake()
    {
        // Obtém o Animator uma vez só
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica camada da bala
        int layer = collision.gameObject.layer;
        if (layer == LayerMask.NameToLayer("PlayerBullet") ||
            layer == LayerMask.NameToLayer("Bala"))
        {
            LoseLife();
        }
    }

    private void LoseLife()
    {
        vidas--;
        if (vidas <= 0)
        {
            Destroy(gameObject);
            return;
        }

        // Ativa isHurt e dispara a coroutine que irá desativá-lo
        animator.SetBool("isHurt", true);
        StartCoroutine(ResetHurtBool(0.5f));  // meio segundo de “hurt”
    }

    private IEnumerator ResetHurtBool(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("isHurt", false);
    }
}
