using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private float moveSpeed = 0.05f;

	void Start()
    {
        Managers.Input.Actions -= PlayerInput;
        Managers.Input.Actions += PlayerInput;
	}
	void Update()
	{

	}

	void PlayerInput()
	{
		if (Managers.Input.TouchDirection != Vector3.zero)
		{
			if(Managers.Input.TouchDirection.x > 0)
			{
				
			}

			transform.position = transform.position + Managers.Input.TouchDirection * moveSpeed;
		}
	}
}
