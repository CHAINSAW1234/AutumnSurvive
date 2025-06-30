using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject resultPanel;
	public Vector3 Position 
	{ 
		get { return transform.position; } 
		set { transform.position = value; ApplyScreenBoundary(); } // if change position, apply boundary
	}

	private Animator animator = null;

    private const float moveSpeed = 8f;

    private void Start()
    {
        animator = GetComponent<Animator>();

        Managers.Input.Actions -= PlayerInput;
        Managers.Input.Actions += PlayerInput;
	}
    private void Update()
	{
	}

	private void ApplyScreenBoundary()
	{
		Vector3 position = transform.position;
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        float x = GetComponent<CapsuleCollider2D>().bounds.extents.x;
        float y = GetComponent<CapsuleCollider2D>().bounds.extents.y;
        position.x = Mathf.Clamp(position.x, min.x + x, max.x - x);
        position.y = Mathf.Clamp(position.y, min.y + y, max.y - y);

		transform.position = position;
    }
	private void PlayerInput()
	{
		if (Managers.Input.TouchDirection != Vector3.zero)
		{
            Vector3 localScale = transform.localScale;
            if (Managers.Input.TouchDirection.x > 0)
			{
				localScale.x = Mathf.Abs(localScale.x);
            }
			else
			{
				localScale.x = Mathf.Abs(localScale.x) * -1f;
            }
			transform.localScale = localScale;

            Position = transform.position + Managers.Input.TouchDirection * Managers.Input.TouchDirectionMagnitude * moveSpeed * Time.deltaTime;
            animator.SetBool("Walk", true);
        }
		else
		{
            animator.SetBool("Walk", false);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Enemy":
                resultPanel.SetActive(true);
                // Finish Game
                break;

        }
    }
}
