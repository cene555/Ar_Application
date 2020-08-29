using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[ExecuteInEditMode]
public class BreakMesh : MonoBehaviour {

	/// <summary>
	/// APPROX Number of parts you want the object to explode into (don't blow up your computer); ex: 5-100 something like that??
	/// </summary>
	const int CHANGE_THIS_FOR_EVERY_OBJECT = 15; 

	private Material objectMat;

	private void Start () {
		Explode ();
	}

	void Explode () {
		//dont run twice and dont run if it has children
		if (transform.childCount == 0) {
			StartCoroutine (SplitMesh ());
		}
	}

	IEnumerator SplitMesh () {
		print ("exploding");
		Mesh m = GetComponent<MeshFilter> ().sharedMesh;
		// Get the vertex liss
		Vector3 [] verts = m.vertices;
		Vector3 [] norms = m.normals;
		Vector2 [] uvs = m.uv;

		// Get the triangles and material for the given submesh
		int [] tris = m.GetTriangles (0);//this would need to be extended for more submeshes
		objectMat = GetComponent<MeshRenderer> ().sharedMaterials [0];
		int numTrisPerNewMesh = Mathf.RoundToInt (tris.Length / (CHANGE_THIS_FOR_EVERY_OBJECT * 3));
		MeshFilter [] meshFilters = new MeshFilter[numTrisPerNewMesh];
		int meshCount = 0;
		// Do for all triangles (increment is always 3)
		for (int i = 0; i < tris.Length; i += 3) {
			//meshFilter
			Mesh newMesh = new Mesh ();
			newMesh.vertices = new Vector3 [] { verts [tris [i]], verts [tris [i + 1]], verts [tris [i + 2]] };
			newMesh.normals = new Vector3 [] { norms [tris [i]], norms [tris [i + 1]], norms [tris [i + 2]] };
			newMesh.uv = new Vector2 [] { uvs [tris [i]], uvs [tris [i + 1]], uvs [tris [i + 2]] };
			newMesh.triangles = new int [] { 0, 1, 2 };
			//create new game object cause I guess we have to?? not entirely sure but dont care since this is only for the editor
			GameObject go = new GameObject ();
			go.name = i.ToString ();
			go.transform.localPosition = Vector3.zero;
			go.transform.localRotation = Quaternion.identity;
			go.transform.localScale = Vector3.one;
			go.transform.SetParent (this.transform);
			go.AddComponent<MeshFilter> ().sharedMesh = newMesh;
			go.AddComponent<MeshRenderer> ().sharedMaterials = new Material [] { objectMat };
			//add to array
			meshFilters [meshCount] = go.GetComponent<MeshFilter>();
			meshCount++;
			//combine meshes
			if (meshCount % numTrisPerNewMesh == 0) {
				CombineMeshes (meshFilters,false);
				meshCount = 0;
				for (int e = 0; e < meshFilters.Length; e++) {
					DestroyImmediate (meshFilters [e].gameObject);
				}
				yield return new WaitForEndOfFrame ();
			}
			yield return null;
		}
		//combine remaining meshes
		CombineMeshes (meshFilters,true);
		yield return new WaitForSeconds (1f);
	}

	void CombineMeshes (MeshFilter [] meshFilters, bool isLast) {
		CombineInstance [] combine = new CombineInstance [meshFilters.Length];
		for (int i = 0; i < meshFilters.Length; i++) {
			if (meshFilters [i] != null) {
				combine [i].mesh = meshFilters [i].sharedMesh;
				combine [i].transform = meshFilters [i].transform.localToWorldMatrix;
			}
		}
		//create new combined mesh
		GameObject go = new GameObject ();
		go.transform.SetParent (this.transform);
		go.transform.localPosition = Vector3.zero;
		go.transform.localEulerAngles = Vector3.zero;
		go.transform.localScale = Vector3.one;
		go.AddComponent<MeshFilter> ();
		go.GetComponent<MeshFilter> ().mesh = new Mesh ();
		go.GetComponent<MeshFilter> ().sharedMesh.CombineMeshes (combine);
		go.AddComponent<MeshRenderer> ().sharedMaterials = new Material [] { objectMat };
		if (isLast) {
			for (int e = 0; e < meshFilters.Length; e++) {
				if (meshFilters [e] != null) {
					DestroyImmediate (meshFilters [e].gameObject);
				}
			}
			//remove this mesh etc.
			DestroyImmediate (GetComponent<Renderer> ());
			DestroyImmediate (GetComponent<MeshFilter> ());
			DestroyImmediate (this);
		}
	}
}