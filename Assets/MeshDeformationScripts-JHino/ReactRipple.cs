using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ReactRipple : MonoBehaviour
{
    [Header("Ripple Settings")]
    [SerializeField] private float amplitude = 0.1f;

    [SerializeField] private float frequency = 10f;
    
    [Tooltip("How fast the ripples travel outward")]
    [SerializeField] private float speed = 5f;
    
    [SerializeField] private float duration = 2f;

    [Tooltip("Local point where the ripple starts")]
    [SerializeField] private Vector3 localEpicenter = Vector3.up;

    private Mesh mesh;
    private Vector3[] baseVertices;
    private Vector3[] modifiedVertices;

    private float rippleTimer = 0f;
    private bool isRippling = false;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        baseVertices = mesh.vertices;
        modifiedVertices = new Vector3[baseVertices.Length];
    }

    private void OnEnable()
    {
        CollisionBroadcaster.OnEntitiesCollided += TriggerRipple;
    }

    private void OnDisable()
    {
        CollisionBroadcaster.OnEntitiesCollided -= TriggerRipple;
    }

    private void TriggerRipple()
    {
        isRippling = true;
        rippleTimer = 0f; 
    }

    private void Update()
    {
        if (!isRippling) return;

        rippleTimer += Time.deltaTime;

        float decay = Mathf.Clamp01(1f - (rippleTimer / duration));

        if (decay <= 0f)
        {
            isRippling = false;
            ResetMesh();
            return;
        }

        for (int i = 0; i < baseVertices.Length; i++)
        {
            float distance = Vector3.Distance(baseVertices[i], localEpicenter);

            float wave = Mathf.Sin((distance * frequency) - (rippleTimer * speed));

            Vector3 vertexNormal = baseVertices[i].normalized;

            modifiedVertices[i] = baseVertices[i] + (vertexNormal * (wave * amplitude * decay));
        }

        mesh.vertices = modifiedVertices;
        mesh.RecalculateNormals();
    }

    private void ResetMesh()
    {
        mesh.vertices = baseVertices;
        mesh.RecalculateNormals();
    }
}