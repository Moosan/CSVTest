using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSample : MonoBehaviour {
    public Material material;
    public float a, b;
    public int Count;
    public float Range;
	// Use this for initialization
	void Start () {
        MeshPoint.MeshSet(a, b, material);

        for (int i = 0; i < Count; i++) {
            for (int j = 0; j < Count; j++) {
                MeshPoint mesh = new MeshPoint(new Vector3(i, Random.value*Range,j));
            }
        }

        MeshPoint.MeshesChain();
        MeshPoint.MakeMeshes();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
