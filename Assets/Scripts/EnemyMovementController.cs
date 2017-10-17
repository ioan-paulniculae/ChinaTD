using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour {

	public GameObject target;

	public float speed = 1.0f;
	public float rotationSpeed = 10.0f;

	public void setTarget(GameObject target) {
		this.target = target;
	}

	private void moveTowardsTarget() {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
	}

	private void lookAtTarget() {
		Vector3 direction = target.transform.position - transform.position;
		transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		moveTowardsTarget ();
		lookAtTarget ();
	}
}
