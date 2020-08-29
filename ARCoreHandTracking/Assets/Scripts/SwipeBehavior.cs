using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeBehavior : MonoBehaviour {

	private int activeChildNum = 0;
	private Vector3 offSet = new Vector3 (3, 0, 0);

	public void LeftButtonDown () {
		if(activeChildNum > 0){
			transform.GetChild (activeChildNum).GetComponent<SwipeChildBehavior> ().SetDesiredPosition (offSet);
			activeChildNum--;
			transform.GetChild (activeChildNum).GetComponent<SwipeChildBehavior> ().SetDesiredPosition (Vector3.zero);
		}
	}

	public void RightButtonDown () {
		if (activeChildNum < transform.childCount - 1) {
			transform.GetChild (activeChildNum).GetComponent<SwipeChildBehavior> ().SetDesiredPosition (-offSet);
			activeChildNum++;
			transform.GetChild (activeChildNum).GetComponent<SwipeChildBehavior> ().SetDesiredPosition (Vector3.zero);
		}
	}
}
