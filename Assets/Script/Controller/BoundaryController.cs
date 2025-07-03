using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    [SerializeField]
    private float offset = 1f;

    private void Start()    // set boundary based on camera
    {
        float vertexExtent = Camera.main.orthographicSize * offset;
        float horizontalExtent = vertexExtent * Screen.width / Screen.height;

        Vector3 center = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0f);
        Vector3 size = new Vector3(horizontalExtent * 2, vertexExtent * 2, 0f);

        transform.position = center;

        foreach(Defines.Boundary boundary in System.Enum.GetValues(typeof(Defines.Boundary)))
        {
            string name = boundary.ToDescription();
            Vector2 direction = Defines.Direction[(int)boundary];
            
            Transform targetTransform = transform.Find(name);
            if (targetTransform == null) // if not exist, create
            {
                GameObject obj = new GameObject(name);
                obj.transform.SetParent(transform, false);
                targetTransform = obj.transform;
            }
            
            GameObject target = targetTransform.gameObject;
            target.tag = gameObject.tag;
            target.layer = gameObject.layer;

            // set position, collider size
            target.transform.position = new Vector3(
                horizontalExtent * direction.x + direction.x * 0.5f, 
                vertexExtent * direction.y + direction.y * 0.5f, 0);

            BoxCollider2D collider = target.GetOrAddComponent<BoxCollider2D>();
            collider.size = new Vector2(
                Mathf.Max(1f, size.x * Mathf.Abs(direction.y)),
                Mathf.Max(1f, size.y * Mathf.Abs(direction.x)));
        }
    }
}
