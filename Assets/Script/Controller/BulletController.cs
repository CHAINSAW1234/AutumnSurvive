using UnityEngine;
using System.Collections;


public class BulletController : MonoBehaviour
{
    public Vector2 direction = new Vector2(0, 1);
    public Vector2 Direction
    {
        get => direction;
        set
        {
            direction = value;
            transform.up = -direction;
        }
    }
    public float MoveSpeed { get; set; }
    // Use this for initialization
    void OnEnable()
    {
        MoveSpeed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * Time.deltaTime * MoveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.transform.tag)
        {
            case "Boundary":
                CollisionDestroy();
                break;
        }
    }

    private void CollisionDestroy()
    {
        Managers.Resource.Destroy(gameObject);
    }
}

