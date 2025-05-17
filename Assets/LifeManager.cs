using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField]
    private int vidas = 3; // Configure este valor no Inspector da Unity

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet") || collision.gameObject.layer == LayerMask.NameToLayer("Bala"))
        {
            vidas--;

            if (vidas <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
