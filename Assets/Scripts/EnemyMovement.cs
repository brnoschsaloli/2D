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

    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
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

        // Só anda se estiver no chão, se "ver" o player, e se estiver longe o suficiente
        if (isGrounded && CanSeePlayer() && distanceToPlayer > 1.5)
        {
            Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isRunning", true);


        }
        else
        {
            Debug.Log("entrou aqui");
            // Debug.Log("a distancia: " + distanceToPlayer);
            rb.linearVelocity = Vector2.zero;
            if (distanceToPlayer <= 1.5 && CanSeePlayer())
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", true);

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
        Vector2 dir = ((Vector2)player.position - (Vector2)transform.position).normalized;
        float dist = Vector2.Distance(transform.position, player.position);

        // <--- Ajuste este valor para as layers do seu projeto!
        int layerMask = LayerMask.GetMask("Default", "Player");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, dist, layerMask);

        Debug.DrawRay(transform.position, dir * dist, Color.red);
        return (true);
        // return (hit.collider != null && hit.collider.CompareTag("Player"));
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