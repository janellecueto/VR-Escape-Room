using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpAndDrop : MonoBehaviour {
	GameObject grabbedObject;
	float grabbedObjectSize;

	GameObject GetMouseHoverObject(float range){
		Vector3 position = gameObject.transform.position;
		RaycastHit raycastsHit;
		Vector3 target = position + Camera.main.transform.forward * range;

		if (Physics.Linecast (position, target, out raycastsHit)) {
			return raycastsHit.collider.gameObject;
		}

		return null;
	}

	void TryGrabObject(GameObject grabObject){
		if (grabObject == null)
			return;
		grabbedObject = grabObject;
		grabbedObjectSize = grabObject.GetComponent<Renderer> ().bounds.size.magnitude;﻿
	}

	void DropObject(){
		if (grabbedObject == null)
			return;
		grabbedObject = null;
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if (grabbedObject == null)
				TryGrabObject (GetMouseHoverObject (5));
			else
				DropObject ();
		}
		if (grabbedObject != null) {
			Vector3 newPos = gameObject.transform.position + Camera.main.transform.forward * grabbedObjectSize;
			grabbedObject.transform.position = newPos;
		}
	}
}
