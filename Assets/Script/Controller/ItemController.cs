using System;
using Unity.VisualScripting;
using UnityEngine;


public class ItemController : MonoBehaviour
{
	private Vector2 moveDirection = Vector2.zero;
	private float moveSpeed = 5f;

	private SpriteRenderer skillSprite;

	private Define.Skill skill;

	void Start()
	{
        moveDirection = GenerateRandom.GenerateRandomDirection(new Vector2(0, -1), 30, 60);

    }

	void Update()
	{
		transform.position = transform.position + new Vector3(moveDirection.x, moveDirection.y, 0) * Time.deltaTime * moveSpeed;
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.transform.tag)
        {
            case "Player":
                CollisionPlayer();
                break;
            case "Boundary":
                CollisionBoundary(collision);
                break;
        }
    }

    private void CollisionBoundary(Collision2D collision)
    {
        ContactPoint2D contactPoint = collision.GetContact(0);
		Vector2 normal = contactPoint.normal;
		Vector2 reflect = Vector2.Reflect(moveDirection, normal);
		moveDirection = reflect;
    }

    private void CollisionPlayer()
    {
		Destroy(gameObject);
    }
}
