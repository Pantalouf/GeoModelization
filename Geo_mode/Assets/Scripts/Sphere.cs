using System;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    private MeshFilter meshFilter;
    private Mesh mesh;

    [SerializeField] int nbM;
    [SerializeField] int nbP;
    [SerializeField] float radius;

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

    private int[] GenerateTriangles()
    {
        triangles = new int[(nbM * 2 + (nbP - 1) * nbM * 2) * 3];
        int k = 0;

        for (int i = 0; i < nbM; i++)
        {
            for (int j = 0; j < nbP - 1; j++)
            {
                int next = (i + 1) % nbM;

                int a = j * nbM + i;
                int b = (j + 1) * nbM + i;
                int c = (j + 1) * nbM + next;
                int d = j * nbM + next;
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
        }

        for (int i = 0; i < nbM; i++)
        {
            int next = (i + 1) % nbM;

            int a = i;
            int b = nbM * nbP;
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

            int a = (nbP - 1) * nbM + i;
            int b = nbM * nbP + 1;
            int c = (nbP - 1) * nbM + next;

            triangles[k] = b;
            k++;
            triangles[k] = a;
            k++;
            triangles[k] = c;
            k++;
        }

        return triangles;
    }

    private Vector3[] GenerateVertices()
    {
        vertices = new Vector3[nbM * nbP + 2];
        for (int i = 0; i < nbM; i++)
        {
            float theta = i * 2 * Mathf.PI / nbM;
            for (int j = 0; j < nbP; j++)
            {
                float phi = Mathf.PI * (j + 1) / (nbP + 1);
                vertices[j * nbM + i] = new Vector3(radius * Mathf.Sin(phi) * Mathf.Cos(theta), radius * Mathf.Cos(phi), radius * Mathf.Sin(phi) * Mathf.Sin(theta));
            }
        }
        vertices[nbM * nbP] = new Vector3(0, radius, 0); 
        vertices[nbM * nbP + 1] = new Vector3(0, -radius, 0); 

        return vertices;
    }

    // Update is called once per frame
    void Update()
    {
        
    }    
}
