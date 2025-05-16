using UnityEngine;

public class CrabAttackHitbox : MonoBehaviour
{
    private bool hasHit = false;

    private void OnEnable()
    {
        hasHit = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasHit)
        {
            PlayerLifeStats player = other.GetComponent<PlayerLifeStats>();
            if (player != null)
            {
                Debug.Log("Player hit");
                player.TakeDamage();
                hasHit = true;
            }
        }
    }
}
