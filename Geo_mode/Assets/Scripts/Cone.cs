using System;
using UnityEngine;

public class Cone : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float height;
    [SerializeField] private int nbM;

    private Mesh mesh;
    private MeshFilter meshFilter;

    private int[] triangles;
    private Vector3[] vertices;


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

    private int[] GenerateTriangles()
    {
        triangles = new int[nbM * 6];
        int k = 0;
        for (int i = 0; i < nbM; i++)
        {
            int next = (i + +1) % nbM;
            triangles[k] = next;
            k++;
            triangles[k] = nbM;
            k++;
            triangles[k] = i;
            k++;
        }

        for (int i = 0; i < nbM; i++)
        {
            int next = (i + +1) % nbM;
            triangles[k] = nbM + 1;
            k++;
            triangles[k] = i;
            k++;
            triangles[k] = next;
            k++;
        }

        return triangles;
    }

    private Vector3[] GenerateVertices()
    {
        vertices = new Vector3[nbM + 2];
        for (int i = 0; i < nbM; i++)
        {
            float angle = i * 2 * Mathf.PI / nbM;
            vertices[i] = new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle));
        }

        vertices[nbM] = Vector3.zero;
        vertices[nbM + 1] = new Vector3(0, height, 0);
        return vertices;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
