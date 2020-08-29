using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ToggleCamera_arisa : MonoBehaviour
{
	public bool c = false;
    public GameObject front;


    void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			c = !c;
           	
			if (c) {
				front.SetActive (true);
			} else {
				front.SetActive (false);
			}
		}
    }
}