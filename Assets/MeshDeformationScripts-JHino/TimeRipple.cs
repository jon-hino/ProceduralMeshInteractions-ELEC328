using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TimeRipple : MonoBehaviour
{
    [Header("Ripple Settings")]
    [SerializeField] private float amplitude = 0.1f;
    
    [SerializeField] private float frequency = 10f;
    
    [Tooltip("How fast the ripples travel outward")]
    [SerializeField] private float speed = 5f;

    [Tooltip("Local point where the ripple starts")]
    [SerializeField] private Vector3 localEpicenter = Vector3.up;

    private Mesh mesh;
    private Vector3[] baseVertices;
    private Vector3[] modifiedVertices;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        baseVertices = mesh.vertices;
        modifiedVertices = new Vector3[baseVertices.Length];
    }
    private void Update()
    {
        for (int i = 0; i < baseVertices.Length; i++)
        {
            float distance = Vector3.Distance(baseVertices[i], localEpicenter);

            float wave = Mathf.Sin((distance + Time.time) * frequency);

            Vector3 vertexNormal = baseVertices[i].normalized;

            modifiedVertices[i] = baseVertices[i] + (vertexNormal * (wave * amplitude));
        }

        mesh.vertices = modifiedVertices;
        mesh.RecalculateNormals();
    }
}