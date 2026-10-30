using NUnit.Framework.Internal;
using System;
using UnityEngine;

public class ConeTronque : MonoBehaviour
{
    [SerializeField] private int nbM;
    [SerializeField] private float radius;
    [SerializeField] private float height;
    [SerializeField] private float heightCut;

    private Mesh mesh;
    private MeshFilter meshFilter;

    private Vector3[] vertices;
    private int[] triangles;

    void Start()
    {
        if (heightCut <= 0f || heightCut >= height)
        {
            Debug.Log("heightCut doit être entre 0 et height.");
            return;
        }

        mesh = new Mesh();
        mesh.name = "GeneratedMesh";

        mesh.vertices = GenerateVertices();
        mesh.triangles = GenerateTriangles();

        mesh.RecalculateNormals();

        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }

    private Vector3[] GenerateVertices()
    {
        float radiusTop = radius * (height - heightCut) / height;

        vertices = new Vector3[nbM * 2 + 2];
        for (int i = 0; i < nbM; i++)
        {
            float angle = i * 2 * Mathf.PI / nbM;
            vertices[i] = new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle));
            vertices[i + nbM + 2] = new Vector3(radiusTop * Mathf.Cos(angle), heightCut, radiusTop * Mathf.Sin(angle));
        }

        vertices[nbM] = Vector3.zero;
        vertices[nbM + 1] = new Vector3(0, heightCut, 0);
        return vertices;
    }

    private int[] GenerateTriangles()
    {
        triangles = new int[nbM * 12];
        int k = 0;
        int offset = nbM + 2;

        for (int i = 0; i < nbM; i++)
        {
            int next = (i + 1) % nbM;
            triangles[k] = next;
            k++;
            triangles[k] = nbM;
            k++;
            triangles[k] = i;
            k++;
        }

        for (int i = 0; i < nbM; i++)
        {
            int next = (i + 1) % nbM;
            triangles[k] = i + offset;
            k++;
            triangles[k] = nbM + 1;
            k++;
            triangles[k] = next + offset;
            k++;
        }

        for (int i = 0; i < nbM; i++)
        {
            int next = (i + 1) % nbM;
            int a = i + offset;
            int b = i;
            int c = next;
            int d = next + offset;

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

        return triangles;
    }
}
