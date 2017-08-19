using UnityEngine;
using System.Collections;

public class SquareMesh 
{
    [SerializeField]private static Material material;
    [SerializeField]private Vector3[] vertices;
    public GameObject GameObject { get; set; }

    void Start()
    {
        Vector3[] vertices = {
            new Vector3(-1f, -1f, 0),
            new Vector3(-1f,  1f, 0),
            new Vector3( 1f,  1f, 0),
            new Vector3( 1f, -1f, 0)
        };

        int[] triangles = { 0, 1, 2, 0, 2, 3 };

        Mesh mesh = new Mesh()
        {
            vertices = vertices,
            triangles = triangles
        };
        mesh.RecalculateNormals();

        MeshFilter meshFilter = GameObject.GetComponent<MeshFilter>();
        if (!meshFilter) meshFilter = GameObject.AddComponent<MeshFilter>();

        MeshRenderer meshRenderer = GameObject.GetComponent<MeshRenderer>();
        if (!meshRenderer) meshRenderer = GameObject.AddComponent<MeshRenderer>();

        meshFilter.mesh = mesh;
        meshRenderer.sharedMaterial = material;
    }
}
