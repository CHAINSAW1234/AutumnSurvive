using System.Linq;
using UnityEngine;


public class ItemController : MonoBehaviour
{
	private Vector2 direction = Vector2.zero;
    private int reflectCount = 0;
    private float moveSpeed;

    private SpriteRenderer skillSprite = null;
    private Defines.Skill skill;
    private Vector2 spriteSize;


    private const float minSpeed = 4f;
    private const float maxSpeed = 8f;
    private const int maxReflectCount = 5;

    private void Awake()
    {
        skillSprite = GetComponentsInChildren<SpriteRenderer>()
              .FirstOrDefault(sr => sr.gameObject != this.gameObject);
        spriteSize = skillSprite.sprite.bounds.size;
        spriteSize = spriteSize * skillSprite.transform.localScale;
    }

    void OnEnable()
	{
        direction = Utils.GetRandomDirection(-Vector2.up, 10, 50);
        reflectCount = 0;
        moveSpeed = UnityEngine.Random.Range(minSpeed, maxSpeed);

        skill = PlayerDataController.Instance.GetRandomEnableSkill();
        skillSprite.sprite = Managers.Resource.Load<Sprite>($"Sprites/" + skill.ToDescription());

        Vector2 newSpriteSize = skillSprite.sprite.bounds.size;
        float largestSide = Mathf.Max(newSpriteSize.x, newSpriteSize.y);
        Vector2 scale = spriteSize / newSpriteSize;

        skillSprite.transform.localScale = Vector3.one * scale;

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
        if(reflectCount >= maxReflectCount)
        {
            Managers.Resource.Destroy(gameObject);
        }
    }

    private void CollisionPlayer()
    {
        Managers.Resource.Destroy(gameObject);
        Managers.Resource.Instantiate(skill.ToDescription(), transform.position);
    }
}
