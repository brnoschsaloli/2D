using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Transform player;

    public float moveSpeed = 2f;



    private Animator animator;

    public PlayerStats playerStats;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        playerStats = player.gameObject.GetComponent<PlayerStats>();
    }

    void Update()
    {
        // if (player == null) return;

        // Checa distância do player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (CanSeePlayer())
        {
            Debug.Log("TO vendo esse malandro");
        }
        // --- FLIP ---
        float xDiff = transform.position.x - player.position.x;
        if (Mathf.Abs(xDiff) > 0.01f)
        {
            float direction = Mathf.Sign(xDiff);
            transform.localScale = new Vector3(direction * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // --- CHECA COLISÃO NA FRENTE ---
        bool isBlocked = false;
        float lookDir = Mathf.Sign(xDiff);
        Vector2 origin = (Vector2)transform.position + Vector2.up * 0.1f; // um pouco acima do centro
        float rayLength = 1.0f;

        // Use camada "Ground" e "Enemy" e "Default" no LayerMask
        int wallMask = LayerMask.GetMask("Player", "Ground", "Enemy");
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.left * lookDir, rayLength, wallMask);
        Debug.DrawRay(origin, Vector2.right * lookDir * (-1) * rayLength, Color.blue);
        if (hit.collider != null)
        {
            isBlocked = true;
        }


        // Só anda se estiver no chão, se "ver" o player, e se estiver longe o suficiente
        if (isGrounded && !isBlocked && CanSeePlayer() && distanceToPlayer > 1.5)
        {
            Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isRunning", true);


        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            if (distanceToPlayer <= 1.5 && CanSeePlayer())
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", true);
                PlayerStats playerStats = gameObject.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.TakeDamage();
                }

            }
            else
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", false);
            }
        }
    }

    private bool CanSeePlayer()
    {

        float xDiff = transform.position.x - player.position.x;

        // --- CHECA COLISÃO NA FRENTE ---
        float lookDir = Mathf.Sign(xDiff);
        Vector2 origin = (Vector2)transform.position + Vector2.up * 0.1f; // um pouco acima do centro
        float rayLength = 10.0f;

        // Use camada "Player" no LayerMask
        int wallMask = LayerMask.GetMask("Player");
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.left * lookDir, rayLength, wallMask);
        Debug.DrawRay(origin, Vector2.right * lookDir * (-1) * rayLength, Color.blue);
        if (hit.collider != null)
        {
            return (true);
        }
        else
        {
            return (false);
        }
        // return (hit.collider != null && hit.collider.CompareTag("Player"));
    }

    void Dodamage()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        Debug.Log("Ataque");
        if (playerStats != null && distanceToPlayer <= 1.5)
        {
            playerStats.TakeDamage();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}