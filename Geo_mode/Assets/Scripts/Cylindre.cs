using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Cylindre : MonoBehaviour
{
    private MeshFilter meshFilter;
    private Mesh mesh;

    [SerializeField] int nbM;
    [SerializeField] float radius;
    [SerializeField] float height;
    
    private Vector3[] vertices;
    private int[] triangles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = new Mesh();
        mesh.name = "GeneratedMesh";

        mesh.vertices = GenerateVertices();
        mesh.triangles = GenerateTriangles();

        mesh.RecalculateNormals();

        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Vector3[] GenerateVertices()
    {
        vertices = new Vector3[nbM * 2 + 2];

        for (int i = 0; i < nbM; i++)
        {
            float angle = i * 2 * Mathf.PI / nbM;
            vertices[i] = new Vector3(radius * Mathf.Cos(angle), height, radius * Mathf.Sin(angle));
            vertices[i + nbM] = new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle));
        }
        vertices[2 * nbM] = new Vector3(0, height, 0);
        vertices[2 * nbM + 1] = Vector3.zero;
        return vertices;
    }

    private int[] GenerateTriangles()
    {
        int k = 0;
        triangles = new int[nbM * 12];

        for (int i = 0; i < nbM; i++)
        {
            int next = (i + 1) % nbM;

            int a = i;
            int b = i + nbM;
            int c = next + nbM;
            int d = next;
            triangles[k] = a;
            k++;
            triangles[k] = c;
            k++;
            triangles[k] = b;
            k++;
            triangles[k] = c;
            k++;
            triangles[k] = a;
            k++;
            triangles[k] = d;
            k++;
        }

        for (int i = 0; i < nbM; i++)
        {
            int next = (i + 1) % nbM;

            int a = i;
            int b = 2 * nbM;
            int c = next;
            triangles[k] = a;
            k++;
            triangles[k] = b;
            k++; 
            triangles[k] = c; 
            k++;
        }

        for (int i = 0; i < nbM; i++)
        {
            int next = (i + 1) % nbM;

            int a = i + nbM;
            int b = 2 * nbM + 1;
            int c = next + nbM;
            triangles[k] = b;
            k++;
            triangles[k] = a;
            k++;
            triangles[k] = c;
            k++;
        }

        return triangles;
    }
}
