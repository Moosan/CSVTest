using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshPoint
{
    private V2 Pos;
    private GameObject gameObject;
    private MeshPoint[] Others;
    private static V2[] OthersPos;
    private int[] triangles;
    private bool IsActive { get; set; }
    private Mesh mesh1;
    private Mesh mesh2;
    [SerializeField] private static Material material;
    private static List<MeshPoint> Meshes;
    private static List<MeshPoint> ActiveMeshes;
    private static float tate;
    private static float yoko;
    // Use this for initialization

    public static void MeshSet(float a,float b,Material material) {
        tate = a;
        yoko = b;
        MeshPoint.material = material;
        OthersPos = new V2[]{
            new V2(0,1),
            new V2(1,1),
            new V2(1,0)
        };
        Meshes = new List<MeshPoint>();
        ActiveMeshes = new List<MeshPoint>();
    }

    public MeshPoint(Vector3 pos) {
        gameObject = new GameObject();
        gameObject.transform.position = new Vector3(pos.x*yoko,pos.y,pos.z*tate);
        Pos = new V2(pos.x,pos.z);
        triangles = new int[] { 0, 1, 2, 0, 2, 3 };
        Meshes.Add(this);
        Others = new MeshPoint[3];
    }

    public static void MeshesChain() {
        for (int i = 0; i < Meshes.Count; i++) {
            Meshes[i].MeshChain();
        }
    }

    private void MeshChain() {
        SerchOthers();
    }
	
	public static void MeshUpdate () {
        foreach (MeshPoint point in ActiveMeshes) {

        }
	}

    private void SerchOthers() {
        int i = 0;
        int j = 0;
        foreach (V2 nextPos in OthersPos) {
            if (Others[i]!=null) {
                i++;
                j++;
                break;
            }
            V2 pos= Pos+nextPos;
            MeshPoint otherMesh=null;
            foreach (MeshPoint point in Meshes) {
                V2 pPos = point.Pos;
                if (pPos.X == pos.X && pPos.Y == pos.Y) {
                    otherMesh = point;
                    j++;
                    break;
                }
            }
            Others[i] = otherMesh;
            i++;
        }
        if (i == j) {
            //Meshes.Remove(this);
            ActiveMeshes.Add(this);
        }
    }

    public static void MakeMeshes() {
        foreach (MeshPoint point in ActiveMeshes) {
            point.MakeMesh();
        }
    }

    private void MakeMesh() {
        Debug.Log("a");
        Vector3 ownPos = gameObject.transform.position;
        Vector3[] verticles1 = {
            new Vector3(),
            Others[0].gameObject.transform.position-ownPos,
            Others[1].gameObject.transform.position-ownPos,
            Others[2].gameObject.transform.position-ownPos
        };
        Vector3[] verticles2 = {
            new Vector3(),
            verticles1[3],
            verticles1[2],
            verticles1[1],
        };
        mesh1 = new Mesh()
        {
            vertices = verticles1,
            triangles = triangles
        };
        mesh2 = new Mesh()
        {
            vertices = verticles2,
            triangles = triangles
        };
        mesh1.RecalculateNormals();
        mesh2.RecalculateNormals();
        //*/
        MeshFilter meshFilter1 = gameObject.GetComponent<MeshFilter>();
        if (!meshFilter1) meshFilter1 = gameObject.AddComponent<MeshFilter>();

        MeshRenderer meshRenderer1 = gameObject.GetComponent<MeshRenderer>();
        if (!meshRenderer1) meshRenderer1 = gameObject.AddComponent<MeshRenderer>();
        
        meshFilter1.mesh = mesh1;
        meshRenderer1.sharedMaterial = material;
        
        //*/
    }
}
