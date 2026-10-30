using System;
using UnityEngine;

public class Plan : MonoBehaviour
{
    private MeshFilter meshFilter;
    private Mesh mesh;

    private Vector3[] vertices;
    private int[] triangles;

    [SerializeField] private int NbLignes;
    [SerializeField] private int NbColonnes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = new Mesh();
        mesh.name = "GeneratedMesh";

        mesh.vertices = GenerateVertices();
        mesh.triangles = GenerateTriangles();

        /*vertices = new Vector3[4];
        triangles = new int[6];

        vertices[0] = new Vector3(-1, 1, 0);
        vertices[1] = new Vector3(1, 1, 0);
        vertices[2] = new Vector3(1, -1, 0);
        vertices[3] = new Vector3(-1, -1, 0);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 3;
        triangles[4] = 0;
        triangles[5] = 2;

        mesh.vertices = vertices;
        mesh.triangles = triangles;*/

        mesh.RecalculateNormals();

        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }

    private int[] GenerateTriangles()
    {
        int k = 0;
        triangles = new int[NbColonnes * NbLignes * 6];
        for (int i = 0; i < NbLignes; i++)
        {
            for (int j = 0; j < NbColonnes; j++)
            {
                int a = i * (NbColonnes + 1) + j;
                int b = a + NbColonnes + 1;
                int c = b + 1;
                int d = a + 1;
                triangles[k] = d;
                k++;
                triangles[k] = a;
                k++;
                triangles[k] = b;
                k++;
                triangles[k] = b;
                k++;
                triangles[k] = c;
                k++;
                triangles[k] = d;
                k++;
            }
        }
        return triangles;
    }

    private Vector3[] GenerateVertices()
    {
        vertices = new Vector3[(NbColonnes + 1) * (NbLignes + 1)];
        for (int i = 0; i <= NbLignes; i++)
        {
            for (int j = 0; j <= NbColonnes; j++)
            {
                vertices[i * (NbColonnes + 1) + j] = new Vector3(j, i, 0);
            }
        }
        return vertices;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
