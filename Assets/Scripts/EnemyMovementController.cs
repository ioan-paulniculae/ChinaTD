using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : EnemyBaseController {

	public float speed = 1.0f;
	public float rotationSpeed = 10.0f;

	private LinkedList<GameObject> pathPoints;
	private LinkedListNode<GameObject> currentTargetNode;
	private GameObject currentTarget;

	override public void ApplyWaveModifier(EnemySpawner.EnemyWaveModifierType modifier) {
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer> ();

		/* TODO: maybe refactor this without hardcoded values. */
		switch (modifier) {
		case EnemySpawner.EnemyWaveModifierType.NONE:
			break;
		case EnemySpawner.EnemyWaveModifierType.AGILE:
			spriteRenderer.color = new Color (0, 255, 0);
			speed *= 2;
			break;
		case EnemySpawner.EnemyWaveModifierType.HARDENED:
			spriteRenderer.color = new Color (0, 0, 255);
			gameObject.GetComponent<EnemyStats> ().health *= 2;
			break;
		case EnemySpawner.EnemyWaveModifierType.SWARMING:
			spriteRenderer.color = new Color (255, 0, 0);
			break;
		}
	}

	override public void setPath(GameObject[] pathPoints) {
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
