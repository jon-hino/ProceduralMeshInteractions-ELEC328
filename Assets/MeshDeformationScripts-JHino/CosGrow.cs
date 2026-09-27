using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CosGrow : MonoBehaviour
{
    [Header("Pulsing Settings")]

    [SerializeField] private float frequency = 2f;


    [SerializeField] private float amplitude = 0.2f;

    [Header("Axis Settings")]
    [SerializeField] private bool scaleX = true;
    [SerializeField] private bool scaleY = true;
    [SerializeField] private bool scaleZ = true;

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
        float cosineValue = Mathf.Cos(Time.time * frequency);
        float scaleFactor = 1f + (cosineValue * amplitude);

        Vector3 axisScale = new Vector3(
            scaleX ? scaleFactor : 1f,
            scaleY ? scaleFactor : 1f,
            scaleZ ? scaleFactor : 1f
        );

        for (int i = 0; i < baseVertices.Length; i++)
        {
            modifiedVertices[i] = Vector3.Scale(baseVertices[i], axisScale);
        }

        mesh.vertices = modifiedVertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}
