using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehavior : MonoBehaviour {

	const float MAX_FRAMES = 25;

	const float explosionForce = .02f;
	const float radius = .011f;
	const float upModifier = 100f;

	private List<Vector3> positions = new List<Vector3> ();
	private List<Quaternion> rotations = new List<Quaternion> ();

	private int frameCount = 0;
	private Rigidbody rb;

	private void Awake () {
		rb = GetComponent<Rigidbody> ();
		positions.Add (transform.localPosition);
		rotations.Add (transform.localRotation);
		rb.AddExplosionForce (explosionForce, transform.localPosition, radius, upModifier, ForceMode.Impulse);
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (positions.Count < MAX_FRAMES) {
			positions.Add (transform.localPosition);
			rotations.Add (transform.localRotation);
			frameCount++;
		} else {
			if (rb != null) {
				DestroyImmediate (rb);
				frameCount = 0;
				transform.localPosition = positions [0];
				transform.localRotation = rotations [0];
			}

			transform.localPosition = Vector3.Lerp (transform.localPosition, positions [frameCount], Time.deltaTime * 10f);
			transform.localRotation = Quaternion.Slerp (transform.localRotation, rotations [frameCount], Time.deltaTime * 10f);

			if (frameCount == 0 && Vector3.Distance (transform.position, positions [0]) < .5f) {
				transform.localPosition = positions [0];
				transform.localRotation = rotations [0];
			}
		}
	}

	public void SetFrameCount (int count) {
		frameCount = count;
	}
}