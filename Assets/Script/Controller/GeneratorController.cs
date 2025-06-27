using System.Collections;
using UnityEngine;
using static Defines;

public class GeneratorController : MonoBehaviour
{
    [SerializeField]
    private GameObject generatingTargetObject = null;

    [SerializeField]
    private float offset = 1f;
    [SerializeField]
    private Boundary boundary;
    [SerializeField]
    private int maxCount = Inf;
    [SerializeField]
    private float generateDelay = 0.1f;

    private Vector2 generatorSize = Vector2.zero;
    private int objectCount = 0;

    private const float sizeBias = 0.8f;
    void Start()
    {
        if(generatingTargetObject == null)
        {
            Managers.Resource.Destroy(gameObject);
            return;
        }

        SetupGeneratePosition();

        StartCoroutine(GenerateGameObject());
    }

    void Update()
    {
    }

    private void SetupGeneratePosition()
    {
        float vertexExtent = Camera.main.orthographicSize * offset;
        float horizontalExtent = vertexExtent * Screen.width / Screen.height;
       
        Vector2 direction = Direction[(int)boundary];
        Vector3 size = new Vector3(horizontalExtent * 2, vertexExtent * 2, 0f);

        transform.transform.position = new Vector3(
            horizontalExtent * direction.x + direction.x * 0.5f,
            vertexExtent * direction.y + direction.y * 0.5f, 0);

        generatorSize = new Vector2(
            Mathf.Max(0.1f, size.x * Mathf.Abs(direction.y) * sizeBias),
            Mathf.Max(0.1f, size.y * Mathf.Abs(direction.x) * sizeBias)); 

    }
    private IEnumerator GenerateGameObject()
    {
        while(true)
        {
            if (objectCount >= maxCount)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(generateDelay);
                Generate();
            }
        }
    }

    private void Generate()
    {

        Vector3 randomPos = new Vector3(
                Random.Range(-generatorSize.x / 2, generatorSize.x / 2),
                Random.Range(-generatorSize.y / 2, generatorSize.y / 2), 0);

        GameObject obj = Managers.Resource.Instantiate(generatingTargetObject.name, transform.position + randomPos);

        AutoActions autoActions = obj.GetOrAddComponent<AutoActions>();
        autoActions.OnDisableEvent -= GeneratedObjectOnDisable;
        autoActions.OnDisableEvent += GeneratedObjectOnDisable;

        ++objectCount;
    }
    private void GeneratedObjectOnDisable()
    {
        objectCount -= 1;
    }
}
