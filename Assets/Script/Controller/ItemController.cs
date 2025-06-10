using System;
using Unity.VisualScripting;
using UnityEngine;


public class ItemController : MonoBehaviour
{
	private Vector2 direction = Vector2.zero;
	private float moveSpeed = 5f;
    private int reflectCount = 0;

	private SpriteRenderer skillSprite;
	private Defines.Skill skill;

    private const int MaxReflectCount = 5;

	void OnEnable()
	{
        reflectCount = 0;
        direction = GenerateRandom.GenerateRandomDirection(new Vector2(0, -1), 10, 50);

    }

	void Update()
	{
		transform.position = transform.position + new Vector3(direction.x, direction.y, 0) * Time.deltaTime * moveSpeed;
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
        if(!Utils.IsInCamera(Camera.main, transform.position))
        {
            return;
        }

        ContactPoint2D contactPoint = collision.GetContact(0);
		Vector2 normal = contactPoint.normal;
		Vector2 reflect = Vector2.Reflect(direction, normal);
		direction = reflect;

        ++reflectCount;
        if(reflectCount >= MaxReflectCount)
        {
            Managers.Resource.Destroy(gameObject);
        }
    }

    private void CollisionPlayer()
    {
		Managers.Resource.Destroy(gameObject);
        // create Skill
    }


}
