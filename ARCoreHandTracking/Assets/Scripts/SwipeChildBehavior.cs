using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeChildBehavior : MonoBehaviour {

	private Vector3 desiredPosition = Vector3.one;

	
	// Update is called once per frame
	void Update () {
		if (desiredPosition != Vector3.one) {
			transform.localPosition = Vector3.Lerp (transform.localPosition, desiredPosition, Time.deltaTime * 6f);
		}
	}

	public void SetDesiredPosition (Vector3 pos) {
		desiredPosition = pos;
	}
}
