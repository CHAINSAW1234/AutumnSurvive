using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private GamePlayController gamePlayController;

    private Vector2 direction = Vector2.zero;

    private float moveSpeed;

    private const float minSpeed = 4f;
    private const float maxSpeed = 8f;

    private const float Radius = 2f;

    void OnEnable()
    {
        gamePlayController = FindAnyObjectByType<GamePlayController>();
     
        Transform player = GameObject.FindWithTag("Player").transform;

        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle * Radius;
        Vector3 randomPosition = player.position + new Vector3(randomDirection.x, randomDirection.y, 0);

        direction = (randomPosition - transform.position).normalized;

        transform.up = -direction;

        moveSpeed = UnityEngine.Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * Time.deltaTime * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.activeInHierarchy == false)
            return;

        switch (collision.transform.tag)
        {
            case "Skill":
                gamePlayController.AddScore(UnityEngine.Random.Range(30, 50));
                Managers.Resource.Instantiate("Explosion", transform.position);
                Managers.Resource.Destroy(gameObject);
                break;
            case "Boundary":
                Managers.Resource.Destroy(gameObject);
                break;
        }
    }

}
