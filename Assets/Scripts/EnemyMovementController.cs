using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour {

	public float speed = 1.0f;
	public float rotationSpeed = 10.0f;

	private LinkedList<GameObject> pathPoints;
	private LinkedListNode<GameObject> currentTargetNode;
	private GameObject currentTarget;

	public void setPath(GameObject[] pathPoints) {
		this.pathPoints = new LinkedList<GameObject>(pathPoints);
		this.currentTargetNode = this.pathPoints.First;
		this.currentTarget = this.currentTargetNode.Value;
	}

	private void setNextTarget() {
		this.currentTargetNode = this.currentTargetNode.Next;
		if (this.currentTargetNode != null) {
			this.currentTarget = this.currentTargetNode.Value;
		}
	}

	private void moveTowardsTarget() {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, step);
	}

	private void lookAtTarget() {
		Vector3 direction = currentTarget.transform.position - transform.position;
		transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log (coll.gameObject);
		if (coll.gameObject.tag == "PathPoint") {
			setNextTarget ();
		}
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
