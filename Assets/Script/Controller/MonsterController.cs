using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Vector2 direction = Vector2.zero;
    private float moveSpeed = 7f;

    private const float Radius = 1f;

    void OnEnable()
    {
        Transform player = GameObject.FindWithTag("Player").transform;
        
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle * Radius;
        Vector3 randomPosition = player.position + new Vector3(randomDirection.x, randomDirection.y, 0);

        direction = (randomPosition - transform.position).normalized;

        transform.up = -direction;
    }

    void Update()
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * Time.deltaTime * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.transform.tag)
        {
            case "Boundary":
            case "Skill":
                CollisionDestroy();
                break;
        }
    }

    private void CollisionDestroy()
    {
        Managers.Resource.Destroy(gameObject);
    }
}
