using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnchor : MonoBehaviour {

	public Transform objectToFollow;

	public Vector3 localOffset;

	public Vector3 screenOffset;

	private RectTransform canvas;

	void Start() {
		canvas = GetComponentInParent<Canvas> ().GetComponent<RectTransform> ();
	}

	void LateUpdate() {

		// Create a "hollow" game object with the same position as the enemy.
		// NOTE: this is required because we want to render the health bar directly above the enemy.
		// If we didn't make this "hack", the health bar's position would depend on the enemy's rotation.
		// By coping the transform and nullifying the rotation, we ensure that it's displayed properly.
		GameObject obj = new GameObject();
		obj.transform.position = objectToFollow.transform.position;
		obj.transform.rotation = Quaternion.identity;

		// Translate our anchored position into world space.
		Vector3 worldPoint = obj.transform.TransformPoint(localOffset);

		// Destroy the "hollow" object.
		Destroy (obj);

		// Translate the world position into viewport space.
		Vector3 viewportPoint = Camera.main.WorldToViewportPoint(worldPoint);

		// Canvas local coordinates are relative to its center, so we offset by half.
		viewportPoint -= 0.5f * Vector3.one;
		viewportPoint.z = 0;

		// Scale position by canvas size.
		Rect rect = canvas.rect;
		viewportPoint.x *= rect.width;
		viewportPoint.y *= rect.height;

		// Apply the new position.
		transform.localPosition = viewportPoint + screenOffset;
	}
}
